using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // [SerializeField] private float verticalInputAcceleration = 40;
    // [SerializeField] private float horizontalInputAcceleration = 40f;
    private float inputAcceleration = 40f;

    [SerializeField] private float maxSpeed = 40f;
    [SerializeField] private float velocityDrag = 3f;

    [SerializeField] private GameObject shipThrust;

    private Vector3 velocity;
    private Vector3 screen_up = new Vector3(0, 1, 0);
    private Vector3 screen_right = new Vector3(1, 0, 0);

    public float boostCooldown = 2.0f;
    public float maxBoostDuration = 0.25f;
    public float boostTime = 0.25f;
    public bool isBoosting = false;

    private void Start()
    {
        shipThrust.SetActive(false);
    }
    private void Awake()
    {

    }
    public void Update()
    {

        // if the booster button is pressed, and the cooldown is OK, and we are not boosting yet - start the booster
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && boostTime >= boostCooldown && !isBoosting)
        {
            isBoosting = true;
            boostTime = maxBoostDuration;
        }
       
        // when the booster runs out, add a cooldown
        if (boostTime <= 0.0f && isBoosting)
        {
            isBoosting = false;
  
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            shipThrust.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            shipThrust.SetActive(false);
        }

        // apply forward input
        Vector3 acceleration_v = Input.GetAxis("Vertical") * inputAcceleration * screen_up; // verticalInputAcceleration * transform.right;

        // apply side-ways input
        Vector3 acceleration_h = Input.GetAxis("Horizontal") * inputAcceleration * screen_right; // horizontalInputAcceleration * transform.up;

        Vector3 acceleration = Vector3.ClampMagnitude(acceleration_h + acceleration_v, inputAcceleration);


        if (!isBoosting)
        {
            velocity += acceleration * Time.deltaTime;
            boostTime = Mathf.Clamp(boostTime + Time.deltaTime, 0, boostCooldown);
        }
        else
        {
            velocity += acceleration * 4.0f * Time.deltaTime;
            boostTime -= Time.deltaTime;
        }
     }

    private void FixedUpdate()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion oldRotation = transform.rotation;

        transform.rotation = Quaternion.Lerp(oldRotation, Quaternion.AngleAxis(angle, Vector3.forward), 0.12f);

        // apply velocity drag
        velocity = velocity * (1 - Time.deltaTime * velocityDrag);

        // clamp to maxSpeed

        if (isBoosting)
        {
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed*3.0f);
        }
        else
        {
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        }


        // update transform
        transform.position += velocity * Time.deltaTime;
    }
}
