using UnityEngine;
using System.Collections;

public class Enemy2Script : EnemyScript
{
    public float decisionTime = 1.5f;
    public float moveTime = 60f;
    public float ballingtime = 5f;

    public GameObject fireballBlueprint;
    GameObject myFireball;

    void Awake()
    {
        GameObject ball = Instantiate(fireballBlueprint);
        myFireball = ball;
    }
    void Start()
    {
        StartCoroutine(NextAction());
    }

    IEnumerator NextAction()
    {
        Debug.Log("is ploting next action");
        int choice = Random.Range(1, 3);
        yield return new WaitForSecondsRealtime(decisionTime);
        if (choice == 1) { StartCoroutine(Move()); }
        else { StartCoroutine(Balling()); }
    }
    IEnumerator Move() 
    {
        Debug.Log("is moving");
        Vector3 direction = new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f), 0f);
        for (int i = 0; i < moveTime; i++) 
        { 
        transform.Translate(direction.normalized * moveTime*Time.deltaTime);
        yield return new WaitForSecondsRealtime(0.05f);
        }
        StartCoroutine(NextAction() );
    }

    IEnumerator Balling()
    {
        if ((myFireball != null) && (myFireball.GetComponent<FireballScript>().isAway = true))
        {
          SummonBall();
            yield return new WaitForSecondsRealtime(ballingtime);
        }
        StartCoroutine(NextAction());
    }

    void SummonBall()
    {
        myFireball.GetComponent<Transform>().position = transform.position;
        myFireball.GetComponent<FireballScript>().isAway = false;
    }
}