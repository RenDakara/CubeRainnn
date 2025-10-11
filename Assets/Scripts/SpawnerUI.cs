using TMPro;
using UnityEngine;

public class SpawnerUIManager<TSpawner, TObject> : MonoBehaviour
    where TSpawner : GenericSpawner<TObject>
    where TObject : MonoBehaviour, IPoolableObject
{
    [SerializeField] protected TSpawner spawner;
    [SerializeField] protected TextMeshProUGUI statsText;

    protected virtual void OnEnable()
    {
        spawner.OnPoolChanged += UpdateUI;
        UpdateUI();
    }

    protected virtual void OnDisable()
    {
        spawner.OnPoolChanged -= UpdateUI;
    }

    protected virtual void UpdateUI()
    {
        statsText.text = $"Active: {spawner.GetActiveCount()}, Created: {spawner.GetCreatedCount()}";
    }
}
