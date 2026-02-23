using UnityEngine;

public class FurnitureScript : MonoBehaviour
{
    public bool isPushed = false;
    void OnCollisionEnter(Collision collision)
    {
        GameObject collided = collision.gameObject;
        if (collided.GetComponent<EnemyScript>() == true && isPushed == true)
        {
            Destroy(collided);
            GameObject player = GameObject.Find("Main Camera");
            
        }
    }
}
