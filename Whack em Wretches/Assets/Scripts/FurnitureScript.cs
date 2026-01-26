using UnityEngine;

public class FurnitureScript : MonoBehaviour
{
    public Furniture reference;
    int mass;
    void Awake()
    {
        mass = reference.mass;
    }
}
