using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //viktig när man jobbar med UI att ha med denna, annars funkar det inte

public class JumpDisplay : MonoBehaviour
{
    public int jump = 0;
    public Text jumpText;
    private Hero gameOverChecker;

    private void Update()
    {
        gameOverChecker = GameObject.Find("Hero").GetComponent<Hero>();

        jumpText.text = jump.ToString(); //man kan också skriva tex healthText.text = "Health :" + health;
        if (Input.GetKeyDown(KeyCode.Space) || gameOverChecker.GameOver != true)
        {
            jump++;
        }
    }
}
