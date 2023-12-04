using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpeed;

    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _targetDirection;

    private void Awake()
    {
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
       
        if (_targetDirection != Vector2.zero)
        {
            MoveForward();
        }
    }

    private void UpdateTargetDirection()
    {
        if (_playerAwarenessController.AwareOfPlayer)
        {
            _targetDirection = _playerAwarenessController.DirectionToPlayer;

        }
        else
        {
            _targetDirection = Vector2.zero;
        }
    }

    private void RotateTowardsTarget()
    {
        if (_targetDirection == Vector2.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        RotateObject(rotation.eulerAngles.z);
    }

    void MoveForward()
    {
        transform.position = Vector2.MoveTowards(transform.position, _playerAwarenessController.GetPlayerTransform().position, _speed*Time.deltaTime);
    }

    void RotateObject(float targetRotation)
    {                                                                                    
        float currentRotation = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.z, targetRotation, _rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, currentRotation);

    }
}
