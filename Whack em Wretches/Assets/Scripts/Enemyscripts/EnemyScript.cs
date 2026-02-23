using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{

    public Transform playerTransform;
    public float speed;
    public float might;
    public GameObject deathanimation;

    void Awake()
    {
        playerTransform = GameObject.Find("Main Camera").GetComponent<Transform>();
    }
    void OnDestroy()
    {
        GameObject mort = Instantiate(deathanimation);
        mort.transform.position = transform.position;
    }
}
