using UnityEngine;

public class BatController : MonoBehaviour
{
    [SerializeField] private int _life = 1;
    [SerializeField] private float _speed = 1;
    [SerializeField] private GameObject _heart;
    [SerializeField] private bool _isBoss = false;

    [HideInInspector] public bool IsAlive = true;

    private Transform _target;
    private Vector3 _targetPosition;
    private bool _reachedDestination = true;
    private Vector3 _startPosition;
    private SpriteRenderer _sprite;
    private int _currentLife = 1;
    private bool _returnToStart = false;

    private void Update() {
        ResetPosition();
        Move();

        _sprite.forceRenderingOff = !IsAlive;
    }

    private void Move() {
        if (GameController.Instance.State != GameState.Run || _target == null) return;

        if (_reachedDestination) {
            _reachedDestination = false;
            _targetPosition = _isBoss && _returnToStart ? _startPosition : _target.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Time.deltaTime * _speed);

        if (transform.position == _targetPosition) {
            _reachedDestination = true;

            if(_isBoss)
                _returnToStart = !_returnToStart;
        }
    }

    public void Die() {
        _currentLife--;
        if(_currentLife <= 0) {
            IsAlive = false;
            SpawnHeart(Random.Range(1, 100));
        }
    }

    private void SpawnHeart(float value) {
        if (value < 50) return;

        _heart.gameObject.SetActive(true);
        _heart.transform.parent = null;
        _heart.transform.position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            _target = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            _target = null;
        }
    }

    private void ResetPosition() {
        if (GameController.Instance.State != GameState.Start) return;

        transform.position = _startPosition;
        IsAlive = true;
        _currentLife = _life;
    }

    private void Awake() {
        _sprite = GetComponent<SpriteRenderer>();
        _startPosition = transform.position;
        _currentLife = _life;
    }
}
