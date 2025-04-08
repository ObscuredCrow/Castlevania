using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void Update() {
        Move();
    }

    private void Move() {
        if (GameController.Instance.State != GameState.Run || _target == null) return;

        var position = _target.position;
        position.y = 0;
        position.z = -10;

        transform.position = position;
    }
}
