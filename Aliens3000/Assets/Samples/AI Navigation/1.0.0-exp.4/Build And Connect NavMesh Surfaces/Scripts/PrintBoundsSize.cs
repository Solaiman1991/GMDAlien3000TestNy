using UnityEngine;

public class PrintBoundsSize : MonoBehaviour
{
    void Start()
    {
        var renderer = GetComponentInChildren<Renderer>();
        Vector3 size = renderer.bounds.size;
        Debug.Log("Size of GameObject: " + size);
    }
}