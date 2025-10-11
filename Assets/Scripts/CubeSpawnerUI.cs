using TMPro;
using UnityEngine;

public class CubeSpawnerUI : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private TextMeshProUGUI _statsText;

    private void OnEnable()
    {
        _cubeSpawner.OnPoolChanged += UpdateUI;
        UpdateUI();
    }

    private void OnDisable()
    {
        _cubeSpawner.OnPoolChanged -= UpdateUI;
    }

    private void UpdateUI()
    {
        _statsText.text = $"Cubes: Active={_cubeSpawner.GetActiveCount()}, Created={_cubeSpawner.GetCreatedCount()}";
    }
}
