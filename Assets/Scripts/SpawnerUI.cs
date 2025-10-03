using TMPro;
using UnityEngine;

public class SpawnerUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cubeStatsText;
    [SerializeField] private CubeSpawner cubeSpawner;

    [SerializeField] private TextMeshProUGUI bombStatsText;
    [SerializeField] private BombSpawner bombSpawner;

    private void Update()
    {
        cubeStatsText.text = $"Cubes: Active={cubeSpawner.GetActiveCount()}, Created={cubeSpawner.GetCreatedCount()}";
        bombStatsText.text = $"Bombs: Active={bombSpawner.GetActiveCount()}, Created={bombSpawner.GetCreatedCount()}";
    }
}
