using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //viktig när man jobbar med UI att ha med denna, annars funkar det inte

public class HealthDisplay : MonoBehaviour
{
    private int health = 0;
    public Text healthText;

    private void Update()
    {
        healthText.text = health.ToString(); //man kan också skriva tex healthText.text = "Health :" + health;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            health++;
        }
    }
}
