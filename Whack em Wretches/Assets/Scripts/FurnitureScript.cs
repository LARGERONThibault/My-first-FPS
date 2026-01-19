using UnityEngine;

public class FurnitureScript : MonoBehaviour
{
    public Furniture reference;
    public int mass;
    void Awake()
    {
        mass = reference.mass;
    }
}
