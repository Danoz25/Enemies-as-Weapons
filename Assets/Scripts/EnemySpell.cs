using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpell : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int initDamage = 40; // Initial Damage from Fireball collision
    public int tickDamage = 2; // Tick damage from Fireball collision
    public int duration = 3;
    public GameObject impactEffect;
    Vector3 myScreenPos;
    public AudioSource audioSource;
    public Collider2D colliderSrc;
    public SpriteRenderer rendererSrc;
    private Vector3 playerPos;
    public float clipLength;
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
        playerPos = Camera.main.WorldToScreenPoint(GameObject.FindGameObjectWithTag("Player").transform.position);
        Vector3 direction = (playerPos - myScreenPos).normalized;
        rb.velocity = new Vector2(direction.x, direction.y) * speed;

    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        
       
        Debug.Log(hitInfo.name); // Logs what the fireball collides with in the console

        // Determine if the fireball connected with an enemy, and if so determine interaction
        PlayerCombat player = hitInfo.GetComponent<PlayerCombat>();
        if (player != null)
        {
            player.PlayertakeDamage(initDamage);
            player.TakeDotDamage(tickDamage, duration);
        }

        audioSource.Play();

        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        GetComponent<Collider2D>().enabled = false;

        clipLength = audioSource.clip.length;
        collided = true;
        // Instantiate(impactEffect, transform.position, transform.rotation); // Create an impact effect on collision

    }

    // Update is called once per frame
    void Update()
    {    

        if (collided)
        {
            if (audioSource.time >= clipLength)
            {
                Destroy(gameObject);
            }
        }
    }
}
