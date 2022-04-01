using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBoltSeeker : MonoBehaviour
{
    public GameObject lightningBolt;
    public EnemyCombat[] enemies;
    public EnemyCombat enemy;

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(null);
        Destroy(gameObject, 0.8f);
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemies = collision.GetComponents<EnemyCombat>();
        if (enemies != null)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                Debug.Log("Length is " + enemies.Length);
                Instantiate(lightningBolt, transform.position, transform.rotation, enemies[i].transform);
            } 
        }
        Destroy(gameObject, 2f);
    }
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemy = collision.GetComponent<EnemyCombat>();
        EnemyCombat temp = enemy;
        if (enemy != null)
        {
            StartCoroutine(Deploy(temp));
        }

    }

    IEnumerator Deploy(EnemyCombat temp)
    {
        yield return new WaitForSeconds(0.7f);
        if (enemy != null)
        {
            Instantiate(lightningBolt, transform.position, transform.rotation, temp.transform);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
