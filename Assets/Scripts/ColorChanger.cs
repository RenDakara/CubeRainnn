using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public void ChangeColor(GameObject obj)
    {
        Color color = Random.ColorHSV();
        Renderer renderer = obj.GetComponent<Renderer>();
        renderer.material.color = color;
    }

    public void Fade(GameObject obj, float alpf)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        Material material = renderer.material;
        Color color = material.color;
        material.color = new Color(color.r, color.g, color.b, alpf);
    }
}
