using TMPro;
using UnityEngine;

public class CubeSpawnerUI : SpawnerUIManager<CubeSpawner, Cube>
{
    protected override void UpdateUI()
    {
        statsText.text = $"Cubes: Active={spawner.GetActiveCount()}, Created={spawner.GetCreatedCount()}";
    }
}
