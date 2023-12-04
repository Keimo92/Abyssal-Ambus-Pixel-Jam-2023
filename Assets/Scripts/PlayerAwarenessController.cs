using UnityEngine;

public class PlayerAwarenessController : MonoBehaviour
{
    public bool AwareOfPlayer;

    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float shootRange = 3.0f;
    public float shootCooldown = 1.0f;
    private float nextShootTime;
    private AudioClip detectPlayerSound;
 


    public Vector2 DirectionToPlayer { get; private set; }

    [SerializeField]
    private float _playerAwarenessDistance = 10.0f;

    private Transform player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        detectPlayerSound = (AudioClip)Resources.Load("detected");
    }

    // Update is called once per frame
    void Update()
    {

        EnemyAI enemyai = GetComponent<EnemyAI>();

        Vector2 enemyToPlayerVector = player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (enemyToPlayerVector.magnitude <= _playerAwarenessDistance)
        {
            if (!AwareOfPlayer) // if it wasn't aware before, make it aware now
            {
                AudioSource audioSrc = FindObjectOfType<AudioSource>();
                audioSrc.PlayOneShot(detectPlayerSound);
            }
            enemyai.enabled = false;
            AwareOfPlayer = true;
            if(distanceToPlayer <= shootRange && Time.time >= nextShootTime)
            {
                Shoot();
                nextShootTime = Time.time + shootCooldown;
            }
        }
        else
        {

            enemyai.enabled = true;
            AwareOfPlayer = false;
        }
    }
    public void Shoot()
    {
        // Instantiate a bullet and set its direction
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
       
        rb.velocity = transform.up * 20f;
       
        Destroy(bullet, 1f);
       
    }

    public Transform GetPlayerTransform()
    {
        return player;
    }
}
