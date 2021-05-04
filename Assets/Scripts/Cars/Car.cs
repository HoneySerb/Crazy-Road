using UnityEngine;

[RequireComponent(typeof(CarCrash))]
[RequireComponent(typeof(Rigidbody))]
public class Car : MonoBehaviour
{
    [SerializeField] private WheelCollider _frontLeftWheelCollider, _frontRightWheelCollider;
    [SerializeField] private WheelCollider _rearLeftWheelCollider, _rearRightWheelCollider;

    [SerializeField] private Transform _frontLeftWheelTransform, _frontRightWheelTransform;
    [SerializeField] private Transform _rearLeftWheelTransform, _rearRightWheelTransform;

    [SerializeField] private float _motorTorque;
    [SerializeField] private float _brakeTorque;
    [SerializeField] private float _maxSteeringAngle;

    [SerializeField] private float _maxSpeed;

    public float Speed => _rigidbody.velocity.magnitude;

    protected float _horizontalInput, _verticalInput;
    protected bool _isBrake = false;

    private float _steeringAngle;

    private Rigidbody _rigidbody;


    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    private void FixedUpdate()
    {
        Steer();
        Accelerate();
        UpdateWheels();
        Brake();
    }

    private void Brake()
    {
        float brakeTorque = _isBrake ? _brakeTorque : 0f;

        _frontLeftWheelCollider.brakeTorque = brakeTorque;
        _frontRightWheelCollider.brakeTorque = brakeTorque;
    }

    private void Steer()
    {
        _steeringAngle = _maxSteeringAngle * _horizontalInput;
        _frontLeftWheelCollider.steerAngle = _steeringAngle;
        _frontRightWheelCollider.steerAngle = _steeringAngle;
    }

    private void Accelerate()
    {
        _frontLeftWheelCollider.motorTorque = _motorTorque * _verticalInput;
        _frontRightWheelCollider.motorTorque = _motorTorque * _verticalInput;

        if (_rigidbody.velocity.magnitude >= _maxSpeed)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * _maxSpeed;
        }
    }

    private void UpdateWheels()
    {
        UpdateWheel(_frontLeftWheelCollider, _frontLeftWheelTransform);
        UpdateWheel(_frontRightWheelCollider, _frontRightWheelTransform);

        UpdateWheel(_rearLeftWheelCollider, _rearLeftWheelTransform);
        UpdateWheel(_rearRightWheelCollider, _rearRightWheelTransform);
    }

    private void UpdateWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position = wheelTransform.position;
        Quaternion rotation = wheelTransform.rotation;

        wheelCollider.GetWorldPose(out position, out rotation);

        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }
}
