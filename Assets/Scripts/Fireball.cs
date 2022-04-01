using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int initDamage = 40; // Initial Damage from Fireball collision
    public int tickDamage = 2; // Tick damage from Fireball collision
    public int duration = 3;

    public GameObject impactEffect;
    public GameObject impactObj;
    Vector3 myScreenPos;
    public AudioSource audioSource;
    public Collider2D colliderSrc;
    public SpriteRenderer rendererSrc;
    public GameObject fireballfab;

    public float clipLength = 0;
    public float clipduration = 0f;
    public bool collided = false;


    public void Awake()
    {
        audioSource = GetComponent<AudioSource>(); 
    }
    // Start is called before the first frame update
    void Start()
    {
        myScreenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        Vector3 direction = (Input.mousePosition - myScreenPos).normalized;
        rb.velocity = new Vector2(direction.x, direction.y) * speed;
        Destroy(gameObject, 1.5f);
    }

    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name); // Logs what the fireball collides with in the console

        // Determine if the fireball connected with an enemy, and if so determine interaction
        EnemyCombat enemy = hitInfo.GetComponent<EnemyCombat>();
        BossFIght boss = hitInfo.GetComponent<BossFIght>();
        if (enemy != null)
        {
            enemy.TakeDamage(initDamage);
            enemy.TakeDotDamage(tickDamage, duration);
        }
        else if (boss.CompareTag("Boss") && boss != null)
        {
            boss.TakeDamage(initDamage);
            boss.TakeDotDamage(tickDamage, duration);
        }
   




        audioSource.Play();

        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        GetComponent<Collider2D>().enabled = false;

        clipLength = audioSource.clip.length;
        collided = true;

        impactObj = Instantiate(impactEffect, transform.position, transform.rotation); // Create an impact effect on collision
        Destroy(impactObj, 1f); // Delete animation after it has ended

    }

    // Update is called once per frame
    void Update()
    {
      
        if(collided)
        {
            if(audioSource.time >= clipLength)
            {
                Destroy(gameObject);
            }
        }
    }


}
