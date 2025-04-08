using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 0.5f;
    [SerializeField] private float _jump = 0.5f;
    [SerializeField] private GameObject[] _whips;

    [HideInInspector] public bool CanMove = true;

    private Animator _animator;
    private Vector3 _startPosition;
    private float _moveVelocity;
    private Rigidbody2D _rigid;
    private bool _isGrounded = true;
    private SpriteRenderer _sprite;

    private void Update() {
        ResetPosition();
        Move();
    }

    private void ResetPosition() {
        if (GameController.Instance.State != GameState.Start) return;

        transform.localPosition = _startPosition;
        _animator.ResetTrigger("Dead");
    }

    private void MoveLevel() {
        transform.localPosition = _startPosition;
    }

    private void Move() {
        if (GameController.Instance.State != GameState.Run || !CanMove) return;

        if (Input.GetKeyDown(KeyCode.F)) {
            _animator.SetTrigger("Attack");
            Invoke("Whip", 0.1f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded) {
            _rigid.linearVelocity = new Vector2(_rigid.linearVelocity.x, _jump);
            _isGrounded = false;
        }

        _moveVelocity = 0;

        GameController.Instance.IsHoldingS = Input.GetKey(KeyCode.S);

        if (Input.GetKey(KeyCode.A)) {
            _moveVelocity = -_speed;
            _sprite.flipX = false;
        }

        if (Input.GetKey(KeyCode.D)) {
            _moveVelocity = _speed;
            _sprite.flipX = true;
        }

        _animator.SetBool("Jumping", !_isGrounded);
        _animator.SetBool("Walking", _moveVelocity != 0);
        _rigid.linearVelocity = new Vector2(_moveVelocity, _rigid.linearVelocity.y);
    }

    private void Whip() {
        if (_sprite.flipX) _whips[1].SetActive(true);
        else _whips[0].SetActive(true);

        Invoke("ResetWhips", 0.5f);
    }

    private void ResetWhips() {
        foreach(var whip in _whips)
            whip.SetActive(false);
    }

    public void Die() {
        _animator.SetTrigger("Dead");        
    }

    private void AllowMoving() => CanMove = true;

    private void Awake() {
        //_audio = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _startPosition = transform.localPosition;
    }

    void OnCollisionEnter2D() {
        _isGrounded = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Door") {
            GameController.Instance.ChangeLevel(1);
            MoveLevel();
        }
    }
}
