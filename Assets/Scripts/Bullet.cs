using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(out Cube cube);
        if (cube) cube.DestroySelf();
    }
}