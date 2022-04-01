using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : MonoBehaviour
{
    public GameObject impactEffect;
    public GameObject impactObj;
    public int damage = 10;
    private Vector3 myScreenPos;
    private GameObject[] enemies;
    private Vector3 enemyPos;
    public Collider2D collisionBox;
    public Rigidbody2D rb;
    public int speed = 20;
    public Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
        var trueScale = new Vector3(
                     0.2f / parent.lossyScale.x,
                     0.4f / parent.lossyScale.y,
                     1f / parent.lossyScale.z);

        transform.localScale = trueScale;
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name); // Logs what the object collides with in the console

        // Determine if the fireball connected with an enemy, and if so determine interaction
        EnemyCombat enemy = hitInfo.GetComponent<EnemyCombat>();
        BossFIght boss = hitInfo.GetComponent<BossFIght>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        if(boss.CompareTag("Boss"))
        {
            boss.TakeDamage(damage);
        }

        //GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        //GetComponent<Collider2D>().enabled = false;

        impactObj = Instantiate(impactEffect, enemy.transform.position, transform.rotation); // Create an impact effect on collision
        Destroy(impactObj, 0.54f); // Delete animation after it has ended
        Destroy(gameObject);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localScale = transform.localScale;

        // Will keep moving the Lightning Bolt Toward the enemy (parent) it is assigned to
        myScreenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        enemyPos = Camera.main.WorldToScreenPoint(parent.transform.position);
        Vector3 direction = (enemyPos - myScreenPos).normalized;
        rb.velocity = new Vector2(direction.x, direction.y) * speed;

        // Rotates the Lightning Bolt while its tracking the enemy
        Vector2 dir = parent.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
