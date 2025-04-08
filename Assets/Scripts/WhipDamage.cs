using UnityEngine;

public class WhipDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Enemy")
            if (collision.GetComponentInParent<BatController>().IsAlive)
                collision.GetComponentInParent<BatController>().Die();
    }
}
