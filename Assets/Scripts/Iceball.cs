using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceball : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int initDamage = 20; // Initial Damage from Fireball collision
    public int freezeDuration = 3;
    public int slowDuration = 3;
    public float slow = 0.3f; // The fraction of the enemy's speed you want it to be slowed to

    public GameObject impactEffect;
    public GameObject impactObj;
    Vector3 myScreenPos;
    //public AudioSource audioSource; // UNCOMMENT WHEN AUDIO SOURCE IS FOUND
    public Collider2D colliderSrc;
    public SpriteRenderer rendererSrc;

    public float clipLength = 0;
    public float clipduration = 0f;
    public bool collided = false;


    public void Awake()
    {
        //audioSource = GetComponent<AudioSource>(); // UNCOMMENT WHEN AUDIO SOURCE IS FOUND
    }
    // Start is called before the first frame update
    void Start()
    {
        myScreenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        Vector3 direction = (Input.mousePosition - myScreenPos).normalized;
        rb.velocity = new Vector2(direction.x, direction.y) * speed;
        Destroy(gameObject, 1.5f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name); // Logs what the fireball collides with in the console

        // Determine if the iceball connected with an enemy, and if so determine interaction
        EnemyCombat enemy = hitInfo.GetComponent<EnemyCombat>();
        BossFIght boss = hitInfo.GetComponent<BossFIght>();
        if (enemy != null)
        {
            enemy.TakeDamage(initDamage);
            enemy.Freeze(slow, freezeDuration, slowDuration);
        }
        if(boss.CompareTag("Boss"))
        {
            boss.TakeDamage(initDamage);
            boss.Freeze(slow, freezeDuration, slowDuration);
        }

        //audioSource.Play(); // UNCOMMENT WHEN AUDIO SOURCE IS FOUND
        //audioSource.Play();

        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        GetComponent<Collider2D>().enabled = false;

        //clipLength = audioSource.clip.length;
        collided = true;

        impactObj = Instantiate(impactEffect, transform.position, transform.rotation); // Create an impact effect on collision
        Destroy(impactObj, 0.41f); // Destroy impact animation after its done
    }

    // Update is called once per frame
    void Update()
    {
        /* // UNCOMMENT WHEN AUDIO SOURCE IS FOUND
        if (collided)
        {
            if (audioSource.time >= clipLength)
            {
                Destroy(gameObject);
            }
        }
        */
    }
}
