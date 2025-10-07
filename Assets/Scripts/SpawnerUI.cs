using TMPro;
using UnityEngine;
using System;

public class SpawnerUIManager<TSpawner, TObject> : MonoBehaviour
    where TSpawner : GenericSpawner<TObject>
    where TObject : MonoBehaviour, IPoolableObject
{
    [SerializeField] private TSpawner spawner;
    [SerializeField] private TextMeshProUGUI statsText;

    private void OnEnable()
    {
        spawner.OnPoolChanged += UpdateUI;
        UpdateUI();
    }

    private void OnDisable()
    {
        spawner.OnPoolChanged -= UpdateUI;
    }

    private void UpdateUI()
    {
        statsText.text = $"Active: {spawner.GetActiveCount()}, Created: {spawner.GetCreatedCount()}";
    }
}
