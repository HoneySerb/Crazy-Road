using UnityEngine;

public class GrassPanel : MonoBehaviour
{
    [SerializeField] private Tree[] _trees;
    [SerializeField] private int _treesQuantity;

    [SerializeField] private float _xOffset;
    [SerializeField] private float _zOffset;

    private float _xScale => transform.localScale.x;
    private float _zScale => transform.localScale.z;


    private void Start()
    {
        for (int i = 0; i < _treesQuantity; i++)
        {
            int index = Random.Range(0, _trees.Length);

            SpawnTree(_trees[index]);
        }
    }

    private void SpawnTree(Tree tree)
    {
        Transform treeTransform = Instantiate(tree, transform).transform;

        treeTransform.localPosition = GetSpawnPosition();
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;

        float xPosition = Random.Range(-_xScale, _xScale);
        float zPosition = Random.Range(-_zScale, _zScale);

        spawnPosition.x += xPosition / _xOffset;
        spawnPosition.z += zPosition / _zOffset;

        return spawnPosition;
    }
}
