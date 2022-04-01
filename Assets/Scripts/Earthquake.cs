using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : MonoBehaviour
{
    public int initDamage = 10;
    public int duration = 10;
    public float elapsed;
    public bool collided;
    EnemyCombat enemy;
    BossFIght boss;
    public GameObject impactEffect;
    public GameObject impactObj;

    // Start is called before the first frame update
    void Start()
    {
        impactObj = Instantiate(impactEffect, transform.position, transform.rotation); // Create an impact effect on collision
        Destroy(impactObj, 0.42f); // Destroy impact animation after its done
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collided = true;
        enemy = collision.GetComponent<EnemyCombat>();
        boss = collision.GetComponent<BossFIght>();
        if (enemy != null)
        {
            enemy.TakeDamage(initDamage);
        }
        if (boss.CompareTag("Boss"))
        {
            boss.TakeDamage(initDamage);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collided = false;
        enemy = null;
    }

    private
    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 1.5f)
        {
            elapsed = elapsed % 1.5f;
            if (enemy != null)
            {
                enemy.TakeDamage(initDamage);
            }
            impactObj = Instantiate(impactEffect, transform.position, transform.rotation); // Create an impact effect on collision
            Destroy(impactObj, 0.33f); // Destroy impact animation after its done
        }
    }
}
