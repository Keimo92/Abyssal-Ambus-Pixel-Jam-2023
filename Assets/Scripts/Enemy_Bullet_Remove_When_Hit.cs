using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet_Remove_When_Hit : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }


}
