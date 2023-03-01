using TMPro;
using UnityEngine;

public class DestroyedCubesCounter : MonoBehaviour
{
    [SerializeField] private CubeSpawner cubeSpawner;
    [SerializeField] private TextMeshProUGUI counterText;
    private int _counter;

    private void Awake()
    {
        cubeSpawner.OnDestroyCube += IncreaseCounter;
    }

    private void IncreaseCounter()
    {
        _counter++;
        counterText.text = _counter.ToString();
    }
}