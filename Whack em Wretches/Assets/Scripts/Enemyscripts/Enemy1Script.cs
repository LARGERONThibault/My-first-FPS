using UnityEngine;
using System.Collections;

public class Enemy1Script : EnemyScript
{
    public bool dealsdamage;
    public float damagecooldown;
    void LateUpdate()
    {
        Vector3 direction = transform.position - playerTransform.position;
        transform.Translate(-direction.normalized * speed * Time.deltaTime);
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
