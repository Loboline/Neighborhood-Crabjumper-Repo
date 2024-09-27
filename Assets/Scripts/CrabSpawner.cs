using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabSpawner : MonoBehaviour
{
    public GameObject Crab;
    public int xPos;
    public int yPos;
    public int enemyCount;


    void Start ()
    {
    StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
{
    while (enemyCount < 50)
    {
        xPos = Random.Range(-30, -29);
        yPos = Random.Range(3, 5);
        Instantiate(Crab, new Vector2(xPos, yPos), Quaternion.identity);
        yield return new WaitForSeconds(10f);
        enemyCount += 1;

    }
}
}
