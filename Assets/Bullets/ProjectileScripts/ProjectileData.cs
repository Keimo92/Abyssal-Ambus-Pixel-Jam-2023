using UnityEngine;

public class ProjectileData : MonoBehaviour
{
    public float speed;
    public float accel = 0.0f;
    public float fireRate;
    public int damage;
    public float precisionDeviation = 0.0f;
    public float maxLifetime = 2.0f;

    public GameObject muzzlePrefab;
    public GameObject hitPrefab;

    public AudioClip shootSound;
    public float minPitch = 1.0f;
    public float maxPitch = 1.0f;
    // public AudioClip hitSound;

    private float precision;

    // Start is called before the first frame update
    void Start()
    {
        precision = Random.Range(-precisionDeviation, precisionDeviation) / 2.0f;
        var euler = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(euler.x + precision, euler.y, euler.z);

        if (muzzlePrefab != null)
        {
            var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            muzzleVFX.transform.forward = gameObject.transform.forward;

            destroyParticleSystems(muzzleVFX);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
        {
            speed += accel * Time.deltaTime;
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            Debug.Log("Missing speed parameter!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // early exit for ignored collisions
        if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "CaveWalls") return;

        // if gameobject is an enemy with an EnemyHealth script, damage it
        if (collision.gameObject.tag == "Enemy")
        {
            IDamageable damageable = collision.gameObject.GetComponent<EnemyHealth>();
            IDamageable rock_damageable = collision.gameObject.GetComponent<RockExplode>();

            if (damageable != null)
            {
                damageable.TakeDamage(damage, true);
            }
            else if (rock_damageable != null)
            {
                rock_damageable.TakeDamage(damage, true);
            }
            else
            {
                Debug.Log(collision.gameObject.name + " doesn't have a health script!");
            }
        }
        
        destroyProjectile(collision);
    }

    private void destroyProjectile(Collision2D collision)
    {
        speed = 0;

        ContactPoint2D contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;


        if (hitPrefab != null)
        {
            var hitVFX = Instantiate(hitPrefab, pos, rot);

            destroyParticleSystems(hitVFX);
        }

        Destroy(gameObject);
    }

    private void destroyParticleSystems(GameObject obj_with_ps)
    {
        var ps = obj_with_ps.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            Destroy(obj_with_ps, ps.main.duration);
        }
        else
        {
            var psChild = obj_with_ps.transform.GetChild(0).GetComponent<ParticleSystem>();
            Destroy(obj_with_ps, psChild.main.duration);
        }
    }
}
