using System.Collections;
using UnityEngine;

public class RockExplode : MonoBehaviour, IDamageable, IColorShiftable
{
    [SerializeField] public int health;

    [SerializeField] GameObject rockPart1;
    [SerializeField] GameObject rockPart2;
    [SerializeField] GameObject rockPart3;
    [SerializeField] Transform rockDrop;
    [SerializeField] private float splitForce = 5f;

    private SpriteRenderer rend;
    private Color defaultColor;

    void Awake()
    {
        rend = gameObject.GetComponentInChildren<SpriteRenderer>();
        defaultColor = rend.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Explode();
            Destroy(gameObject);
        }
    }


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.collider.gameObject.tag == "Bullet")
    //    {
    //        health -= damage;
    //    }
    //}


    void Explode()
    {
        GameObject rockExplode1 = Instantiate(rockPart1, rockDrop.position, rockDrop.rotation);
        GameObject rockExplode2 = Instantiate(rockPart2, rockDrop.position, rockDrop.rotation);
        GameObject rockExplode3 = Instantiate(rockPart3, rockDrop.position, rockDrop.rotation);

        rockExplode1.transform.localScale = new Vector3(15, 15, 1);
        rockExplode2.transform.localScale = new Vector3(15, 15, 1);
        rockExplode3.transform.localScale = new Vector3(15, 15, 1);

        Rigidbody2D rb1 = rockPart1.GetComponent<Rigidbody2D>();
        rb1.AddForce(new Vector2(-splitForce, splitForce), ForceMode2D.Impulse);
        Rigidbody2D rb2 = rockPart2.GetComponent<Rigidbody2D>();
        rb2.AddForce(new Vector2(splitForce, splitForce), ForceMode2D.Impulse);
        Rigidbody2D rb3 = rockPart3.GetComponent<Rigidbody2D>();
        rb3.AddForce(new Vector2(-splitForce, splitForce), ForceMode2D.Impulse);

        Destroy(rockExplode1, 2f);
        Destroy(rockExplode2, 2f);
        Destroy(rockExplode3, 2f);
    }

    public void TakeDamage(int damage, bool allowDrops)
    {
        health -= damage;

        ShiftColor(rend, Color.red, 0.1f);
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
}
