using UnityEngine;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour
{
    public Slider slider;
    public GameObject player;
    void Start()
    {
        slider.maxValue = player.GetComponent<PlayerScript>().health;
    }

    void Update()
    {
        slider.value = player.GetComponent<PlayerScript>().health;
    }
}
