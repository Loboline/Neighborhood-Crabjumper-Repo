using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabPatrol : MonoBehaviour
{

    public float speed;
    public float distance;
    private bool movingRight = true;
    public Transform groundDetection;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreLayerCollision(9, 9);

        //nedan funktion sköter movement, skjuter strålar framför GameObjektet som den är satt på samt byter riktning om det inte finns något framför.
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if(groundInfo.collider == false)
        {
            if(movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0); //denna rad sätter movement direction att ändras med 180 grader
                movingRight = false;
            } else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
}
