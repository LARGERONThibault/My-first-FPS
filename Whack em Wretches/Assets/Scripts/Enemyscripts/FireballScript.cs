using Unity.VisualScripting;
using UnityEngine;

public class FireballScript : EnemyScript
{
    GameObject creator;
    public bool isAway;
    void SendAway()
    {
        transform.position = new Vector3(9999999, 9999999999999, 9999999);
        isAway = true;
    }
    void Start()
    {
        SendAway();
    }
    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed*Time.deltaTime);
    }

    void OnTriggerEnter(Collider collision)
    {
        GameObject collided = collision.gameObject;
        if (collided.GetComponent<PlayerScript>() == true) 
        {
            collided.GetComponent<PlayerScript>().health -= might;
            SendAway();
        }
    }

}
