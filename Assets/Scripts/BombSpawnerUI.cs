using TMPro;
using UnityEngine;

public class BombSpawnerUI : MonoBehaviour
{
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private TextMeshProUGUI _statsText;

    private void OnEnable()
    {
        _bombSpawner.OnPoolChanged += UpdateUI;
        UpdateUI();
    }

    private void OnDisable()
    {
        _bombSpawner.OnPoolChanged -= UpdateUI;
    }

    private void UpdateUI()
    {
        _statsText.text = $"Bombs: Active={_bombSpawner.GetActiveCount()}, Created={_bombSpawner.GetCreatedCount()}";
    }
}