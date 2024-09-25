using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam;
    public float parallaxEffect;
    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((playerTransform.position.x > -22) && (playerTransform.position.x < 22)) { //Denna if-satsen fixade att parallaxscrollen fortsatte efter världens slut
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.z, transform.position.z); //Rör objekt som har skriptet i parallaxaffect förhållande till kameran, genom ett värde som man skriver in efter scripet har satts på objectet
        }
    }
}
