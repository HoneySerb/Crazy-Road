using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private TrafficCar[] _cars;
    [SerializeField] private int _minCarsQuantity, _maxCarsQuantity;
    [SerializeField] private float _carDistance;

    private float _xScale => transform.localScale.x;


    private void Start()
    {
        int quantity = Random.Range(_minCarsQuantity, _maxCarsQuantity);
        for (int i = 0; i < quantity; i++)
        {
            int carIndex = Random.Range(0, _cars.Length);

            Instantiate(_cars[carIndex], GetSpawnPosition(i), Quaternion.identity);
        }
    }

    private Vector3 GetSpawnPosition(int index)
    {
        Vector3 spawnPosition = transform.position;

        spawnPosition.x += Random.Range(-_xScale, _xScale);
        spawnPosition.z += _carDistance * index;

        return spawnPosition;
    }
}
