using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Transform player;
    public float maxVisibilityDistance = 10f;

    void Update()
    {
        // Calculate the direction from the enemy to the player
        Vector3 direction = player.position - transform.position;

        // Cast a ray from the enemy towards the player
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;

        // Check if the ray hits something
        if (Physics.Raycast(ray, out hit, maxVisibilityDistance))
        {
            // If the hit object is not the player, hide the enemy
            if (hit.collider.gameObject != player.gameObject)
            {
                // Enemy is not in sight, so hide or deactivate it
                gameObject.SetActive(false);
            }
            else
            {
                // Enemy is in sight, so make sure it's active
                gameObject.SetActive(true);
            }
        }
        else
        {
            // If the ray doesn't hit anything, hide the enemy
            gameObject.SetActive(false);
        }
    }
}