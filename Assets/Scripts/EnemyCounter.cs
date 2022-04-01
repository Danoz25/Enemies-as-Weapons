using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public float enemyCounter;
    private Transform playerPos;
    public float spawnDist = 30f;
    public Transform itemSpawn;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCounter > 0 && Vector2.Distance(playerPos.position, itemSpawn.position) <= spawnDist)
        {
            //lock doors
        }
        else if (enemyCounter == 0 && !this.CompareTag("BossRoom"))
        {
            //unlock door
        }
    }
}
