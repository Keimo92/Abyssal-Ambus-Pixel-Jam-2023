using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IDamageable, IColorShiftable
{
    public int maxHealth = 100;
    public int health;

    [SerializeField] GameObject weapon;
    [SerializeField] Transform dropOut;
    private int spawnLayer = 8; // 8 = objects
    private Animator animator;
    public string triggerName;
    [SerializeField] GameObject[] objectsToReveal;

    public GameObject enemyHealthDisplay;
    public AudioSource audiosource;
    public AudioClip clip;

    private bool isDestroyed = false;
    private Text displayValue;

    private SpriteRenderer rend;
    private Color defaultColor;


    private void Awake()
    {
        health = maxHealth;
        displayValue = GameObject.FindGameObjectWithTag("EnemyHealthBar").GetComponentInChildren<Text>();

        rend = gameObject.GetComponent<SpriteRenderer>();

        defaultColor = rend.color;

        foreach (GameObject obj in objectsToReveal)
        {
            obj.SetActive(false);
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage, bool allowDrops = true)
    {
        if (damage > 0)
        {
            ShiftColor(rend, Color.red, 0.1f);
        }
        else
        {
            ShiftColor(rend, Color.green, 0.1f);
        }

        health = Mathf.Clamp(health - damage, 0, maxHealth);
        

        defaultColor = rend.color;

        displayValue.text = HealthText();
        // StartCoroutine(ResetEnemyHealthText());

        if (health <= 0 && !isDestroyed)
        {
            audiosource.PlayOneShot(clip, 0.5f);
            isDestroyed = true;

            foreach (GameObject obj in objectsToReveal)
            {
                obj.SetActive(true);
            }

            if (allowDrops)
            {
                DropLoot();
            }


            if (!string.IsNullOrEmpty(triggerName))
            {
                animator.SetTrigger(triggerName);
            }

            PlayerAwarenessController playerAwareness = GetComponent<PlayerAwarenessController>();
            if (playerAwareness)
            {
                playerAwareness.enabled = false;
            }

            EnemyAggro enemyAggro = GetComponent<EnemyAggro>();
            if (enemyAggro)
            {
                enemyAggro.enabled = false;
            }

            EnemyAI enemyAI = GetComponent<EnemyAI>();
            if (enemyAI)
            {
                enemyAI.enabled = false;
            }

            //Destroy(gameObject);
        }
    }

    void DropLoot()
    {
        if (weapon != null)
        {
            GameObject weaponDrop = Instantiate(weapon, dropOut.position, dropOut.rotation);
            weaponDrop.layer = spawnLayer;
            isDestroyed = true;
        }
    }

    public void RemoveSprite()
    {
        Destroy(gameObject); // that doesn't sound right??? it should destroy the sprite not the object
    }


    public void ShiftColor(SpriteRenderer renderer, Color targetColor, float duration = 0.0f)
    {
        renderer.color = targetColor;

        if (duration > 0)
        {
            StartCoroutine(SwitchColorBack(renderer, duration, defaultColor));
        }
    }
    public IEnumerator SwitchColorBack(SpriteRenderer renderer, float duration, Color originalColor)
    {
        yield return new WaitForSeconds(duration);
        renderer.color = originalColor;
    }

    //public IEnumerator ResetEnemyHealthText()
    //{
    //    yield return new WaitForSeconds(2.0f);
    //    displayValue.text = string.Empty;
    //}
    //
    private string HealthText()
    {
        return string.Format("{0}\n{1}//{2}", gameObject.name, health, maxHealth);
    }
}