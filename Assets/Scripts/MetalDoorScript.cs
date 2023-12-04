using UnityEngine;

public class MetalDoorScript : MonoBehaviour
{
    public GameObject trigger;

    public GameManager manager;
    

    private void Start()
    {
       trigger.SetActive(false);
    }

    public void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (trigger.activeSelf && collision.collider.gameObject.tag == "Player")
        {
            Vector2 oldPosition = collision.gameObject.transform.position;
            Vector2 newPosition = oldPosition;
            newPosition.x += 10;

            collision.gameObject.transform.SetPositionAndRotation(newPosition, transform.rotation);

            manager.StartBossFight();
        }
    }
}
