using UnityEngine;

public class RepairKit : MonoBehaviour
{
    public int increase = 50;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            IDamageable playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(-increase, false);
            }

            Destroy(gameObject);
        }
    }
}
