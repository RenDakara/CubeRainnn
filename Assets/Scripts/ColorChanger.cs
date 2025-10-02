using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public void ChangeColor(GameObject obj)
    {
        Color color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        Renderer renderer = obj.GetComponent<Renderer>();
        renderer.material.color = color;
    }
}
