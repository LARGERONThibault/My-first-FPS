using UnityEngine;

public class EntityScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform cameraTransform;
    void Start()
    {
        cameraTransform = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = new Quaternion(0, -cameraTransform.rotation.y, 90,0);
    }
}
