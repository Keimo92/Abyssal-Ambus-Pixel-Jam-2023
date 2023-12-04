using UnityEngine;

public class KeyCollected : MonoBehaviour
{
    
    [SerializeField] private Transform metalDoor;
    [SerializeField] float openingSpeed;
    bool shouldOpen;
    
    private Vector2 target;
    private void Start()
    {
        target = new Vector2(-69.42f, -142.94f);
        
       
    }



    private void Update()
    {
        

        if (shouldOpen == true)
        {

            OpenDoor();

        }

        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            shouldOpen = true;
        }
    }
   

    public void OpenDoor()
    {
        // Disabled, sorry, I want to trap the player!
       // metalDoor.transform.position = Vector2.MoveTowards(transform.position, target, openingSpeed * Time.deltaTime);
    }
}
