using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public LayerMask visionMask;
    public float visionRange = 5f;
    public GameObject visionMaskObject;
    void Update()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Cast a ray from the player towards the mouse position
        RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePosition - transform.position, visionRange, visionMask);

        // If the ray hits something on the visionMask layer, reveal that area
        if (hit.collider != null)
        {
            Debug.DrawLine(transform.position, hit.point, Color.green);

            // Calculate the direction from the player to the hit point
            Vector3 direction = (hit.point =- transform.position).normalized;

            // Adjust the visibility based on the hit point
            ResetVisibility();

          

            // Add your logic to reveal the area or adjust visibility here
        }
        else
        {
            // If nothing is hit, you might want to handle this differently (e.g., set vision to default state)
            Debug.DrawLine(transform.position, transform.position + (mousePosition - transform.position).normalized * visionRange, Color.red);
            AdjustVisibility(hit.point);
            // Reset visibility to default state when nothing is hit
          
        }
    }




    void AdjustVisibility(Vector3 hitPoint)
    {
        // Example: Adjust the alpha channel of the vision mask based on distance
        float distanceToHit = Vector3.Distance(transform.position, hitPoint);
        float normalizedDistance = Mathf.Clamp01(distanceToHit / visionRange);
        Color visionMaskColor = visionMaskObject.GetComponent<SpriteRenderer>().color;
        visionMaskColor.a = 1 - normalizedDistance; // Adjust alpha inversely proportional to distance
        visionMaskObject.GetComponent<SpriteRenderer>().color = visionMaskColor;

        // You can add more complex logic or use shaders for a smoother effect
    }

    void ResetVisibility()
    {
        // Example: Reset the alpha channel of the vision mask to its default state
        Color visionMaskColor = visionMaskObject.GetComponent<SpriteRenderer>().color;
        visionMaskColor.a = 1f; // Set alpha to fully opaque
        visionMaskObject.GetComponent<SpriteRenderer>().color = visionMaskColor;
        
        // You may need additional logic to handle other visibility adjustments
    }
}

   

