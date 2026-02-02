using UnityEngine;

public class EnemyVisualManager : MonoBehaviour
{
    public Transform playerTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerTransform = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
            transform.position = transform.parent.position;
            transform.LookAt(playerTransform.position);
    }
}
