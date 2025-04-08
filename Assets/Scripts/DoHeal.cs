using UnityEngine;

public class DoHeal : MonoBehaviour
{
    [SerializeField] private Transform _parent;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            GameController.Instance.RaiseHealth();
            ResetHeart();
        }
    }

    public void ResetHeart() {
        transform.parent = _parent;
        transform.position = Vector3.zero;
        gameObject.SetActive(false);
    }
}
