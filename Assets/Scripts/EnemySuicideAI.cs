using UnityEngine;

public class EnemySuicideAI : MonoBehaviour
{
    public float moveSpeed = 9.0f;
    public float rotationSpeed = 130.0f;
    public int damageToPlayer = 10;

    private Vector2 targetDirection;

    public AudioSource audiosource;
    public AudioClip clip;

    private Transform playerTransform;
    private LayerMask PLAYER_LAYER, CAVE_LAYER;
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;
    bool destroyed;
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        PLAYER_LAYER = LayerMask.GetMask("Player");
        CAVE_LAYER = LayerMask.GetMask("CaveWalls");
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
        }

        if (playerTransform == null)
        {
            Debug.LogError("No object found with 'Player' Tag!");
        }
    }

    public Transform GetPlayerTransform()
    {
        return playerTransform;
    }

    private void FixedUpdate()
    {
        if (!PlayerSpotted()) return;

        RotateTowardsTarget();
        MoveForward();
    }

    private bool PlayerSpotted()
    {
        Vector2 enemyToPlayerVector = playerTransform.position - transform.position;

        RaycastHit2D caveWall = Physics2D.Raycast(transform.position, enemyToPlayerVector, 15.0f, CAVE_LAYER);
        RaycastHit2D playerInRange = Physics2D.Raycast(transform.position, enemyToPlayerVector, 15.0f, PLAYER_LAYER);

        if (!playerInRange)
        {
            return false;
        }


        if (caveWall && caveWall.distance < playerInRange.distance)
        {
            return false;
        }

        targetDirection = playerTransform.position - transform.position;
        return true;
    }

    private void RotateTowardsTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        RotateObject(rotation.eulerAngles.z);
    }

    void MoveForward()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }

    void RotateObject(float targetRotation)
    {
        float currentRotation = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.z, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, currentRotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !destroyed)
        {
            audiosource.PlayOneShot(clip);
            IDamageable damageable = collision.gameObject.GetComponent<PlayerHealth>();
            IDamageable suicideHealth = gameObject.GetComponent<EnemyHealth>();
            destroyed = true;
            if (damageable != null)
            {
                damageable.TakeDamage(damageToPlayer, false);
                rb.isKinematic = true;
                moveSpeed = 0;
                rotationSpeed = 0;
                suicideHealth.TakeDamage(999999999, false);
                
                //Destroy(gameObject);
                
            }
            else
            {
                Debug.Log(collision.gameObject.name + " doesn't have a health script!");
            }
        }
    }
}
