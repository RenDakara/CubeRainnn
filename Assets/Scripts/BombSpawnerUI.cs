using TMPro;
using UnityEngine;

public class BombSpawnerUI : SpawnerUIManager<BombSpawner, Bomb>
{
    protected override void UpdateUI()
    {
        statsText.text = $"Bombs: Active={spawner.GetActiveCount()}, Created={spawner.GetCreatedCount()}";
    }
}