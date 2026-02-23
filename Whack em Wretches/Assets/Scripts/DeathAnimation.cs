using System.Collections;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSecondsRealtime(1);
        Destroy(this.gameObject);
    }
    void Awake()
    {
        StartCoroutine(SelfDestruct());
    }
}
