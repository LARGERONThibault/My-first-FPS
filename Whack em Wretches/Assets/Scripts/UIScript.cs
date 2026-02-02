using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    public GameObject player;
    float health;
    bool pushAvailable;
    bool pullAvailable;
    TMP_Text display;

    void Awake()
    {
        display = GetComponent<TMP_Text>();
    }
    // Update is called once per frame
    void Update()
    {
        health = player.GetComponent<PlayerScript>().health;
        pushAvailable = player.GetComponent<PlayerScript>().pushAvailable;
        pullAvailable = player.GetComponent<PlayerScript>().pullAvailable;
        display.SetText("Life : " + health + "\nPush : " + pushAvailable + "\nPull : " + pullAvailable);
    }
}
