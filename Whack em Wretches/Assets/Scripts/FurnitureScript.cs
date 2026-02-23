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
            StopCoroutine("Pushing");
            player.GetComponent<PlayerScript>().ShutdownSafety(this.gameObject, 150, 25,0);
        }

        if (collided.GetComponent<FireballScript>() == true && isPushed == true)
        {
            collided.GetComponent<FireballScript>().SendAway();
            GameObject player = GameObject.Find("Main Camera");
            StopCoroutine("Pushing");
            player.GetComponent<PlayerScript>().ShutdownSafety(this.gameObject, 150, 25, 0);
        }


    }
}
