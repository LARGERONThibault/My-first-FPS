using UnityEngine;

public class Enemy1VisualManager : MonoBehaviour
{
    public Transform playerTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            transform.position = transform.parent.position;
            transform.LookAt(playerTransform.position);
    }
}
