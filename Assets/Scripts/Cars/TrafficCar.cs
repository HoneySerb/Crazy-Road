using UnityEngine;

public class TrafficCar : Car
{
    [SerializeField] private LayerMask _mask;

    private CarSpace _carSpace;


    private void Start() => _carSpace = new CarSpace(transform, _mask);

    private void Update() => _carSpace.UpdateSpace();

    private void LateUpdate()
    {
        Align();

        if (_carSpace.IsFront)
        {
            _verticalInput = 1f;
            _isBrake = false;
        }
        else
        {
            if (_carSpace.IsLeftLane())
            {
                ChangeLane(true);
            }
            else if (_carSpace.IsRightLane())
            {
                ChangeLane(false);
            }
            else
            {
                _isBrake = true;
            }
        }
    }

    private void ChangeLane(bool isLeft)
    {
        float angle = Speed / _carSpace.GetFrontObjectDistance(10f);

        _horizontalInput = isLeft ? -angle : angle;
    }

    private void Align()
    {
        _isBrake = false;

        float angle = transform.rotation.y;

        _horizontalInput = angle != 0f ? angle * -2f : 0f;

        _verticalInput = Mathf.Clamp((1f / Mathf.Abs(angle)), -1f, 1f);
    }
}


public class CarSpace
{
    public bool IsFront { get; private set; }
    
    private bool IsFrontFrontLeft;
    private bool IsFrontFrontRight;
    private bool IsFrontLeft;
    private bool IsFrontRight;
    private bool IsLeft;
    private bool IsRight;

    private readonly LayerMask _mask;
    private readonly Transform _transform;


    public CarSpace(Transform transform, LayerMask mask)
    {
        _transform = transform;
        _mask = mask;
    }

    public void UpdateSpace()
    {
        Vector3 origin = GetSumVectors(_transform.position, Vector3.up * 0.5f);

        IsFront = IsFreeSpace(origin, new Vector3(0f, origin.y, 180f), 10f) &&
                  IsFreeSpace(GetSumVectors(origin, Vector3.right * -1f), new Vector3(0f, origin.y, 180f), 8f) &&
                  IsFreeSpace(GetSumVectors(origin, Vector3.right), new Vector3(0f, origin.y, 180f), 8f);

        IsFrontFrontLeft = IsFreeSpace(origin, new Vector3(-60f, origin.y, 190f), 9f);
        IsFrontFrontRight = IsFreeSpace(origin, new Vector3(60f, origin.y, 190f), 9f);

        IsFrontLeft = IsFreeSpace(origin, new Vector3(-60f, origin.y, 120f), 7.5f);
        IsFrontRight = IsFreeSpace(origin, new Vector3(60f, origin.y, 120f), 7.5f);

        IsLeft = IsFreeSpace(origin, _transform.right * -90f, 5f);
        IsRight = IsFreeSpace(origin, _transform.right * 90f, 5f);
    }

    public float GetFrontObjectDistance(float rayDistance)
    {
        Vector3 origin = GetSumVectors(_transform.position, Vector3.up * 0.5f);
        Vector3[] origins =
        {
            origin,
            GetSumVectors(GetSumVectors(origin, Vector3.right * -1f), Vector3.right * -1f),
            GetSumVectors(GetSumVectors(origin, Vector3.right), Vector3.right)
        };

        Vector3 direction = _transform.TransformDirection(new Vector3(0f, origin.y, 180f));
        for(int i = 0; i < origins.Length; i++)
        {
            Ray ray = new Ray(origins[i], direction);

            Physics.Raycast(ray, out RaycastHit hit, rayDistance, _mask);
            if (hit.distance != 0f)
            {
                return hit.distance;
            }
        }

        return 0f;
    }

    public bool IsLeftLane() => IsLeft && IsFrontLeft && IsFrontFrontLeft;

    public bool IsRightLane() => IsRight && IsFrontRight && IsFrontFrontRight;

    private bool IsFreeSpace(Vector3 origin, Vector3 direction, float distance)
    {
        direction = _transform.TransformDirection(direction);

        Ray ray = new Ray(origin, direction);

        return !Physics.Raycast(ray, distance, _mask);
    }

    private Vector3 GetSumVectors(Vector3 a, Vector3 b) 
    {
        a.x += b.x;
        a.y += b.y;
        a.z += b.z;

        return a;
    }
}
