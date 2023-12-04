using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //Movement//
    public Transform[] waypoints;
    private int nextWaypointID = 0;

    public float moveSpeed = 1.0f;
    public float rotationSpeed = 80.0f;

    private bool doesLoop = true;
    private float nextShootTime;
    private const float waypointEpsilon = 1.0f;
    private const float angleEpsilon = 2.0f;
   
    void Update()
    {
        if (waypoints.Length == 0) return;

        Transform targetTransform = waypoints[nextWaypointID];

        var distanceToWaypoint = (transform.position - targetTransform.position).sqrMagnitude;

        // move to waypoint (distance)
        if (distanceToWaypoint > waypointEpsilon) 
        {
            MoveObject(targetTransform.position);
            // Debug.LogFormat("moving, remaining: {0}", distanceToWaypoint);
        }
        // rotate
        else if (Mathf.Abs(transform.rotation.eulerAngles.z - targetTransform.rotation.eulerAngles.z) > angleEpsilon)
        {
            RotateObject(targetTransform.rotation.eulerAngles.z);
            
            
            // Debug.LogFormat("rotating, src: {0}, trg: {1}", transform.rotation.eulerAngles.z, targetTransform.rotation.eulerAngles.z);
        }
        else if(doesLoop)
        {
            // make sure the rotation is corrected
            transform.rotation = targetTransform.rotation;

            // once the last waypoint is reached, return to the first
            nextWaypointID = (nextWaypointID+1) % waypoints.Length; 
            // Debug.LogFormat("next wp: #{0}", nextWaypointID);
        }
        else
        {
            // Debug.Log("done");
            return;
        }

        // Enemy behavior



    }

    void MoveObject(Vector2 targetPosition)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    void RotateObject(float targetRotation)
    {                                                                                     // This number is the value of rotation Axis, you can use rotateA to get the ship back to origin
        float currentRotation = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.z, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, currentRotation);
        
    }

    void RotateObjectBack(float targetRotation)
    {                                                                                   // This number is the value of rotation Axis, you can use rotateB to get the ship back to origin
        float currentRotation = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.z, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, currentRotation);
    }

}
