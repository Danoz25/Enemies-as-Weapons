using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Transform itemSpawn;
    public GameObject[] itemArray;
    public float spawnDist = 30f;
    public Transform parent;
    private Transform playerPos;
    public bool usedUp = false;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(playerPos.position, itemSpawn.position) <= spawnDist && !parent.CompareTag("StoreRoom") && !usedUp)
        {
            GameObject item = Instantiate(itemArray[Random.Range(0, itemArray.Length)], itemSpawn.position, itemSpawn.rotation);
            usedUp = true;
        }
    }
}
