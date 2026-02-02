using UnityEngine;
using System.Collections;

public class Enemy1Script : MonoBehaviour
{

    public Transform playerTransform;
    public float speed;
    public float might;
    public bool dealsdamage;
    public float damagecooldown;
    void Update()
    {
        transform.Translate(Vector3.MoveTowards(transform.position, playerTransform.position,speed)*Time.deltaTime);
    }
    

    void OnCollisionEnter(Collision collision)
    {
        GameObject collided = collision.gameObject;
        if (collided.GetComponent<PlayerScript>() == true && dealsdamage == true)
        {
            collided.GetComponent<PlayerScript>().health -= might;
            dealsdamage = false;
            StartCoroutine(DamageCooldown());
        }
    }

    IEnumerator DamageCooldown()
    {
        for (int i = 0; i < damagecooldown; i++)
        {
            yield return new WaitForSecondsRealtime(1);
            i++;
        }
        dealsdamage = true;
    }
}
