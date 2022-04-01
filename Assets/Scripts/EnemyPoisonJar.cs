using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoisonJar : MonoBehaviour
{
    public Vector3 LaunchOffset;
    public float speed = 20f;
    public float arcHeight = 1;
    public Rigidbody2D rb;
    public int initDamage = 1; // Initial Damage from Fireball collision
    public int tickDamage = 5; // Tick damage from Fireball collision
    public int duration = 10;
    public GameObject impactEffect;

    Vector3 targetPos;
    Vector3 myScreenPos;
    Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        myScreenPos = this.transform.position;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 direction = (playerPos - myScreenPos).normalized;
        //rb.AddForce(transform.forward * speed, ForceMode2D.Force);
        rb.velocity = new Vector2(direction.x, direction.y) * speed;



    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name); // Logs what the fireball collides with in the console

        // Determine if the poison jar connected with an enemy, and if so determine interaction
        PlayerCombat enemy = hitInfo.GetComponent<PlayerCombat>();
        if (enemy != null)
        {
            enemy.PlayertakeDamage(initDamage);
            enemy.TakeDotDamage(tickDamage, duration);
        }

        // Instantiate(impactEffect, transform.position, transform.rotation); // Create an impact effect on collision
        Destroy(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        // transform.position += -transform.right * speed * Time.deltaTime;
        /*
        // Compute the next position, with arc added in
        float x0 = startPos.x;
        float x1 = targetPos.x;
        float dist = x1 - x0;
        float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
        float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - x0) / dist);
        float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
        Vector3 nextPos = new Vector3(nextX, baseY + arc, transform.position.z);

        // Rotate to face the next position, and then move there
        transform.rotation = LookAt2D(nextPos - transform.position);
        transform.position = nextPos;
        //rb.MovePosition(startPos + nextPos * Time.deltaTime * speed);

        if (nextPos == targetPos) Arrived();
        */
    }

    void Arrived()
    {
        Destroy(gameObject);
    }
    static Quaternion LookAt2D(Vector2 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }
}
