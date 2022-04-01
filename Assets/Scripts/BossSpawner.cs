using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{

    public Transform itemSpawn;
    public GameObject[] itemArray;
    public bool spawnedIn =false;
    // Start is called before the first frame update
    void Start()
    {
       
          
    }

    // Update is called once per frame
    void Update()
    {
         if (this.CompareTag("BossRoom") && !spawnedIn)
        {
            GameObject item = Instantiate(itemArray[Random.Range(0, itemArray.Length)], itemSpawn.position, itemSpawn.rotation);
            spawnedIn = true;
        }
    }
}
