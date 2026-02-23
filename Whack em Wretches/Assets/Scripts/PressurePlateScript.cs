using UnityEngine;
using System.Collections;

public class PressurePlateScript : MonoBehaviour
{
    public Material noCooldown;
    public Material onCooldown;
    public bool hasCooldown = true;
    public float cooldown;
    public float damage;

    IEnumerator Cooldown()
    {
        hasCooldown = false;
        GetComponent<MeshRenderer>().material = onCooldown;
        yield return new WaitForSecondsRealtime(cooldown);
        hasCooldown = true;
        GetComponent<MeshRenderer>().material = noCooldown;
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject collided = other.gameObject;
        if (hasCooldown == true)
        {

            if (collided.GetComponent<PlayerScript>() == true)
            {
                collided.GetComponent<PlayerScript>().health -= damage;
                StartCoroutine(Cooldown());
            }

            else if (collided.GetComponent<EnemyScript>() == true)
            {
                Destroy(collided);
                StartCoroutine(Cooldown());
            }
        }
    }
}
