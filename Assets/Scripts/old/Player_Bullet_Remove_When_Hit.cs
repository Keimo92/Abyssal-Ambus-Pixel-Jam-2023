using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet_Remove_When_Hit : MonoBehaviour
{
    // Start is called before the first frame update










    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
