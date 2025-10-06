using TMPro;
using UnityEngine;

public class SpawnerUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _cubeStatsText;
    [SerializeField] private CubeSpawner _cubeSpawner;

    [SerializeField] private TextMeshProUGUI _bombStatsText;
    [SerializeField] private BombSpawner _bombSpawner;

    private void Update()
    {
        _cubeStatsText.text = $"Cubes: Active={_cubeSpawner.GetActiveCount()}, Created={_cubeSpawner.GetCreatedCount()}";
        _bombStatsText.text = $"Bombs: Active={_bombSpawner.GetActiveCount()}, Created={_bombSpawner.GetCreatedCount()}";
    }
}
