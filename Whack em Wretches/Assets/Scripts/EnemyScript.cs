using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{

    public Transform playerTransform;
    public float speed;
    public float might;

    void Awake()
    {
        playerTransform = GameObject.Find("Main Camera").GetComponent<Transform>();
    }
}
