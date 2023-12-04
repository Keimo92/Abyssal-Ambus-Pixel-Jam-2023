using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldEnemyAI : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 1.0f;

    public float rotationSpeed;

    public float rotateA;
    public float rotateB;

    private bool isMovingToB = true;

    void Update()
    {
        if (isMovingToB)
        {
            MoveObject(pointB.position);
            RotateObject(pointA.rotation.eulerAngles.z);
            
        }
        else
        {
            MoveObject(pointA.position);
            RotateObjectBack(pointB.rotation.eulerAngles.z);

        }

        // Check if the object has reached its destination
        if (Vector2.Distance(transform.position, (isMovingToB ? pointB.position : pointA.position)) < 0.1f)
        {
            // Switch direction
            isMovingToB = !isMovingToB;
        }
    }

    void MoveObject(Vector2 targetPosition)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    void RotateObject(float targetRotation)
    {                                                                                     // This number is the value of rotation Axis, you can use rotateA to get the ship back to origin
        float currentRotation = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.z, rotateA, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, currentRotation);
    }

    void RotateObjectBack(float targetRotation)
    {                                                                                   // This number is the value of rotation Axis, you can use rotateB to get the ship back to origin
        float currentRotation = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.z, rotateB, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, currentRotation);
    }


}
