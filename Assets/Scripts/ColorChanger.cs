using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public void ChangeColor(GameObject obj)
    {
        Color color = Random.ColorHSV();
        Renderer renderer = obj.GetComponent<Renderer>();
        renderer.material.color = color;
    }
}
