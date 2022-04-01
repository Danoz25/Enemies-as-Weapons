using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spell : MonoBehaviour
{
    public Transform firePoint;
    public GameObject arrowfab;
    public GameObject[] Abilities;
    public Boolean onetimespell = false;
    public int CurrentSpell;

    public float attackRate = 2f;
    float nextAttack = 0f;


    void Start()
    {
        // gets 1 spell for player on load 
        GenerateRandomSpell();
    }

    public void GenerateRandomSpell()
    {
        // happens only once 
        if (onetimespell == false)
        {
            CurrentSpell = Random.Range(0, Abilities.Length);
            //CurrentSpell = 2; // DEBUGGING
            onetimespell = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // cooldown timer for next attack 
        if (Time.time >= nextAttack)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                // left mouse button and spawns the game object and transform the position
                switch (CurrentSpell)
                {
                    //In the case of Earthquake and Lightning, Spawn spell where the mouse is clicked
                    case 3:
                    case 4:
                        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
                        Vector3 adjustZ = new Vector3(worldPoint.x, worldPoint.y, transform.position.z);
                        Instantiate(Abilities[CurrentSpell], adjustZ, Quaternion.identity);
                        break;
                    // In the case of Adrenaline, spawn the spell as a child object with the Player as the parent
                    case 5:
                        Instantiate(Abilities[CurrentSpell], transform.position, Quaternion.identity, transform);
                        break;
                    // For every other spell, spawn projectile on the player
                    default:
                        Instantiate(Abilities[CurrentSpell], transform.position, Quaternion.identity);
                        break;
                } // end switch
                nextAttack = Time.time + 1f / attackRate;
            }
        }

        if (Time.time >= nextAttack)
        {
            // right mouse button and spawns an arrow 
            if (Input.GetMouseButtonDown(1))
            {
                Arrow();
                nextAttack = Time.time + 1f / attackRate;
            }
        }
    }

    
     void Arrow()
    {
        GameObject arrowshoot = Instantiate(arrowfab, transform.position, Quaternion.identity);
        Destroy(arrowshoot, 1.0f);
    }
}
