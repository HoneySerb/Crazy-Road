using UnityEngine;

public class DestroyPanel : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    private const float _yPosition = -5f;


    private void Update() => transform.position = new Vector3(0f, _yPosition, _playerTransform.transform.position.z);

    private void OnTriggerEnter(Collider other) => Destroy(other.gameObject);
}
