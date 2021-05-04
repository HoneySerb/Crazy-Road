using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private float _minScale;
    [SerializeField] private float _maxScale;


    private void Start()
    {
        SetScale();     

        transform.eulerAngles = Vector3.up * Random.Range(0f, 360f);
    }

    private void SetScale()
    {
        float scaleValue = Random.Range(_minScale, _maxScale);

        Vector3 targetScale = Vector3.one * scaleValue;

        transform.localScale = GetDividedVector(targetScale, transform.parent.localScale);
    }

    private Vector3 GetDividedVector(Vector3 a, Vector3 b)
    {
        a.x /= b.x;
        a.y /= b.y;
        a.z /= b.z;

        return a;
    }
}
