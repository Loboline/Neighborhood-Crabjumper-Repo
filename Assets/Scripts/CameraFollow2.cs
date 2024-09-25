using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2 : MonoBehaviour
{
    public Transform playerTransform;
    public float smoothSpeed = 0.125f; //fördröjning på follow


    void Start() //körs 1 gång vid start
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //letar rätt på positionen för Gameobjektet som har taggen "Player". Sätt det/skapa hyfsat överst under rätt objekt i Unity 

    }

    void FixedUpdate() //körs 1 gång per frame
    {
        Vector3 temp = transform.position; //transform.position är kamerans nuvarande pos (eftersom detta script är gjort att sitta på cameran)

        temp.x = playerTransform.position.x;

        transform.position = temp; //vi sätter tillbaka camerans temp pos till camerans nuvarande pos, efter förändringarna. 

        //vi kan INTE skriva transform.position.x = playerTransform.position.x; för värdet på transform.position kan inte moddas direkt. MEN som en temp var kan den det.

    }
} //class