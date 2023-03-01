using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    [SerializeField] [Range(0, 10)] private float movementSpeed;
    [SerializeField] [Range(0, 20)] private float horizontalBorder;
    [SerializeField] [Range(0, 20)] private float verticalBorder;
    [SerializeField] [Range(0, 20)] private float swiftDashTimerMin;
    [SerializeField] [Range(0, 40)] private float swiftDashTimerMax;
    private CubeSpawner _cubeSpawner;
    private Vector3 _currentDestinationPoint;
    private float _swiftDashTimer;
    private float _currentSwiftDashTimer;

    private void Awake()
    {
        _currentDestinationPoint = GenerateDestinationPoint();
        _swiftDashTimer = 0.0f;
        _currentSwiftDashTimer = Random.Range(swiftDashTimerMin, swiftDashTimerMax);
    }

    private void Start()
    {
        _cubeSpawner = GetComponentInParent<CubeSpawner>();
    }

    private void Update()
    {
        Move();
        SwiftDashTimer();
    }

    private void MoveToDestinationPoint(Vector3 destinationPoint)
    {
        Vector3 position = transform.position;
        transform.position = Vector3.MoveTowards(position, destinationPoint, movementSpeed * Time.deltaTime);
        transform.LookAt(Vector3.Lerp(transform.position, destinationPoint, Time.deltaTime));
    }

    private void SwiftDash(Vector3 destinationPoint)
    {
        transform.position = Vector3.Lerp(transform.position, destinationPoint, movementSpeed * Time.deltaTime);
    }

    private Vector3 GenerateDestinationPoint()
    {
        float x = Random.Range(-horizontalBorder, horizontalBorder);
        float z = Random.Range(-verticalBorder, verticalBorder);
        Vector3 destinationPoint = new Vector3(x, transform.position.y, z);
        return destinationPoint;
    }

    private void Move()
    {
        if (transform.position == _currentDestinationPoint)
        {
            _currentDestinationPoint = GenerateDestinationPoint();
        }


        MoveToDestinationPoint(_currentDestinationPoint);
    }

    private void SwiftDashTimer()
    {
        _swiftDashTimer += Time.deltaTime;
        if (_swiftDashTimer > _currentSwiftDashTimer)
        {
            SwiftDash(_currentDestinationPoint);
            if (transform.position == _currentDestinationPoint)
            {
                _currentSwiftDashTimer = Random.Range(swiftDashTimerMin, swiftDashTimerMax);
                _swiftDashTimer = 0.0f;
            }
        }
    }

    public void DestroySelf()
    {
        _cubeSpawner.OnDestroyCube?.Invoke();
        Destroy(gameObject);
    }
}