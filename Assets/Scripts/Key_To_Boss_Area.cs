using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_To_Boss_Area : MonoBehaviour
{

    [SerializeField] GameObject trigger;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
       
      
        if (collision.gameObject.tag == "Player")
        {

            trigger.SetActive(true);
            Destroy(gameObject);

        }
    }

}
