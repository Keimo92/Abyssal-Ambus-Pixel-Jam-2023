using UnityEngine;

public class EnemyBulletLogic : MonoBehaviour
{
    public int damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Player")
        {
            IDamageable damageable = collision.gameObject.GetComponent<PlayerHealth>();

            if (damageable != null)
            {
                damageable.TakeDamage(damage, true);
            }
            else
            {
                Debug.Log(collision.collider.gameObject.name+" doesn't have a health script!");
            }

            Destroy(gameObject);
        }

        if(collision.collider.gameObject.tag == "CaveWalls")
        {
            Destroy(gameObject);
        }
    }
}
