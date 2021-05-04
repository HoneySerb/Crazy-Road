using UnityEngine;

public class PlayerCar : Car
{
    [SerializeField] private float _speedToBonus;
    [SerializeField] private float _bonus;

    [SerializeField] private FloatEvent _scoreEvent;

    private void Update()
    {
        if (Speed >= _speedToBonus)
        {
            _scoreEvent.Invoke(_bonus);
        }
    }

    private void LateUpdate()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        _isBrake = Input.GetKey(KeyCode.Space);
    }
}
