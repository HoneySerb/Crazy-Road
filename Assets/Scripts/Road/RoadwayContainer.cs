using System.Collections.Generic;
using UnityEngine;

public class RoadwayContainer : MonoBehaviour
{
    [SerializeField] private GameObject _roadway;
    [SerializeField] private int _startQuantity;

    private const float _spawnDistance = 120f;
    private int _spawnedRoadways = -1;

    private readonly List<GameObject> _roadwaysList = new List<GameObject>();


    public void ChangeRoadways()
    {
        SpawnRoadway();

        DestroyRoadway();
    }

    private void Start()
    {
        for (int i = 0; i < _startQuantity; i++)
        {
            SpawnRoadway();
        }
    }

    private void SpawnRoadway()
    {
        GameObject roadway = Instantiate(_roadway, transform, false);

        roadway.transform.position = Vector3.forward * _spawnDistance *_spawnedRoadways;

        roadway.SetActive(true);

        _roadwaysList.Add(roadway);

        _spawnedRoadways++;
    }

    private void DestroyRoadway()
    {
        Destroy(_roadwaysList[0].gameObject);

        _roadwaysList.RemoveAt(0);
    }
}
