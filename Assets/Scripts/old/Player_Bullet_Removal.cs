using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet_Removal : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        
    }
}
