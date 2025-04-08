using UnityEngine;

public class DoDamage : MonoBehaviour
{
    [SerializeField] private BatController _bat;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player" && _bat.IsAlive)
            GameController.Instance.LowerHealth();
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Player" && _bat.IsAlive)
            GameController.Instance.LowerHealth();
    }
}
