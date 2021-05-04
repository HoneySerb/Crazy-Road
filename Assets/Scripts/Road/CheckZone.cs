using UnityEngine;

public class CheckZone : MonoBehaviour
{
    [SerializeField] private float _bonusScore;

    [SerializeField] private FloatEvent _completeZoneEvent;

    private Collider _collider;


    private void Awake() => _collider = GetComponent<Collider>();

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponentInParent<RoadwayContainer>().ChangeRoadways();

            _completeZoneEvent.Invoke(_bonusScore);

            _collider.enabled = false;
        }
    }
}
