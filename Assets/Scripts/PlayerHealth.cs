using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable, IColorShiftable
{
    public int maxHealth;
    private int health;
    private Animator anim;
    private SpriteRenderer rend;
    public string triggerName;
    [SerializeField] GameObject deathScreen;

    private Color defaultColor;
    private Text displayValue;
    public AudioSource audi;
    public AudioClip clip;

    private void Start()
    {
        rend = gameObject.GetComponentInChildren<SpriteRenderer>();
        displayValue = GameObject.FindGameObjectWithTag("PlayerHealthBar").GetComponentInChildren<Text>();

        defaultColor = rend.color;

        health = maxHealth;
        displayValue.text = HealthText();
        anim = GetComponent<Animator>();
        deathScreen.SetActive(false);
    }
    public void RemoveSprite()
    {

        Destroy(gameObject, 05f); // that doesn't sound right??? it should destroy the sprite not the object
    }
    IEnumerator BackToMain()
    {

        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("MainMenu");
    }
    public void TakeDamage(int damage, bool allowDrops)
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
        displayValue.text = HealthText();
        if (health <= 0)
        {
            audi.PlayOneShot(clip);
            StartCoroutine(BackToMain());
            deathScreen.SetActive(true);
            if (!string.IsNullOrEmpty(triggerName))
            {

                anim.SetTrigger(triggerName);


            } // we probably need to do a lot more than just destroy... load menu etc.
        }
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


    private string HealthText()
    {
        return string.Format("Health\n{0}//{1}", health, maxHealth);
    }
}
