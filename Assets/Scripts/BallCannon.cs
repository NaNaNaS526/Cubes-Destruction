using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallCannon : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float angleInDegrees;
    [SerializeField] private List<Image> ballAmountImages;
    [SerializeField][Range(0, 10)] private float rechargeTime;
    private int _currentBallsAmount = 5;

    private Transform _spawnTransform;
    private readonly float _g = Physics.gravity.y;

    private void Awake()
    {
        _spawnTransform = transform;
        StartCoroutine(SpawnBulletsTimer());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (_currentBallsAmount <= 0) return;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var targetHit, 50)) return;
        Vector3 fromTo = targetHit.point - transform.position;
        Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

        transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);


        float x = fromToXZ.magnitude;
        float y = fromTo.y;

        float angleInRadians = angleInDegrees * Mathf.PI / 180;

        float v2 = (_g * x * x) /
                   (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        GameObject newBullet = Instantiate(bullet, _spawnTransform.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody>().velocity = _spawnTransform.forward * v;
        _currentBallsAmount--;
        ballAmountImages[_currentBallsAmount].enabled = false;
    }

    private IEnumerator SpawnBulletsTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(rechargeTime);
            if (_currentBallsAmount < 5)
            {
                ballAmountImages[_currentBallsAmount].enabled = true;
                _currentBallsAmount++;
            }
        }
    }
}