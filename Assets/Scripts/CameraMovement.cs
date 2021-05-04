using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _offset;

    [SerializeField] private float _followSpeed;
    [SerializeField] private float _lookSpeed;

    [SerializeField] private float _xRotation;


    private void FixedUpdate()
    {
        Follow();
        LookAt();
    }

    private void Follow()
    {
        Vector3 target = _player.position + 
                        _player.forward * _offset.z + 
                        _player.right * _offset.x + 
                        _player.up * _offset.y;

        transform.position = Vector3.Lerp(transform.position, target, _followSpeed * Time.deltaTime);
    }

    private void LookAt()
    {
        Vector3 direction = _player.transform.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

        Quaternion targetRotation = Quaternion.Lerp(transform.rotation, rotation, _lookSpeed * Time.deltaTime);

        transform.rotation = new Quaternion(_xRotation, targetRotation.y, targetRotation.z, targetRotation.w);
    }
}
