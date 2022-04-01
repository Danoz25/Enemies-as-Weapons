using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int initDamage = 40; // Initial Damage from collision
    //public int tickDamage = 2; // Tick damage from collision
    public int duration = 3;

    public GameObject impactEffect;
    public GameObject impactObj;
    Vector3 myScreenPos;
    public Collider2D colliderSrc;
    public SpriteRenderer rendererSrc;

    public float clipLength = 0;
    public float clipduration = 0f;
    public bool collided = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<ImpactAudioHandler>();
        Destroy(gameObject, 0.74f);
        //Invoke("GetComponentInChildren<LightningBoltSeeker>()", 0.7f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name); // Logs what the object collides with in the console

        // Determine if the fireball connected with an enemy, and if so determine interaction
        EnemyCombat enemy = hitInfo.GetComponent<EnemyCombat>();
        BossFIght boss = hitInfo.GetComponent<BossFIght>();
        if (enemy != null)
        {
            enemy.TakeDamage(initDamage);
        }
        if(boss.CompareTag("Boss"))
        {
            boss.TakeDamage(initDamage);
        }

        //GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        //GetComponent<Collider2D>().enabled = false;
        collided = true;

        impactObj = Instantiate(impactEffect, enemy.transform.position, transform.rotation); // Create an impact effect on collision
        Destroy(impactObj, 0.54f); // Delete animation after it has ended

    }

    // Update is called once per frame
    void Update()
    {

    }


}
