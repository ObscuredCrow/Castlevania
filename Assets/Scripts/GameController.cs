using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private BoxCollider2D[] _stairs;
    [SerializeField] private GameObject[] _levels;

    public int Life = 5;

    [HideInInspector] public GameState State = GameState.Start;
    [HideInInspector] public bool IsHoldingS;

    private bool _canLowerHealth = true;

    private void Update() {
        StartGame();
        ResetGame();
        WalkableStairs();
    }

    public void StartGame() {
        if (Input.GetKeyDown(KeyCode.Space) && State == GameState.Start) {
            State = GameState.Run;
        }
    }

    public void StopGame() { 
        State = GameState.Stop;
    }

    public void ResetGame() {
        if (Input.GetKeyDown(KeyCode.R) && State == GameState.Stop) {
            State = GameState.Start;
            Life = 5;

            foreach (var heart in FindObjectsByType<DoHeal>(FindObjectsSortMode.None))
                heart.ResetHeart();
        }
    }

    public void WalkableStairs() {
        foreach (var stair in _stairs)
            stair.isTrigger = IsHoldingS;
    }

    public void ChangeLevel(int level) {
        foreach (var lev in _levels)
            lev.SetActive(false);

        _levels[level].SetActive(true);
    }

    public void LowerHealth() {
        if (!_canLowerHealth) return;

        _canLowerHealth = false;
        Life--;
        if (Life <= 0) {
            FindFirstObjectByType<PlayerController>().Die();
            StopGame();
        }

        Invoke("AllowLowerHealthAgain", 0.5f);
    }

    private void AllowLowerHealthAgain() => _canLowerHealth = true;

    public void RaiseHealth() {
        Life++;
        if (Life > 5)
            Life = 5;
    }

    private void Awake() { 
        Instance ??= this;
    }
}