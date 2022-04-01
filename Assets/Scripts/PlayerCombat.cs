using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    
    public Animator animator;
    public Transform attackpointright;
    public Transform attackpointup;
    public Transform attackpointleft;
    public Transform attackpointdown;
    public LayerMask enemyLayers;
    private Vector2 moveDirection;

    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public float attackRate = 2f;
    private float nextAttack = 0f;

    public int maxPlayerHealth = 100;
    public int currentPlayerHealth;
    public healthbar playerhealthbar;

    public float deathTime = 1f;
    public bool animationTime = false;
    public int totem = 0;
    bool playerDeath = false;

    public int wealth = 0;
    public AudioSource audioSrc;
    public AudioClip coinClip;
    public AudioClip healthClip;
    public AudioClip damageClip;
    public AudioClip deathClip;

    public Text currencyUI;

    public Text totemNum;
    public GameObject totemPanel;

    public int attackBoostTotal = 0;
    public Text abNum;
    public GameObject abPanel;
    public int abduration = 20;
    public int abstrengthMult = 2;
    // Start is called before the first frame update
    public void Start()
    {
        // sets player health
        currentPlayerHealth = maxPlayerHealth;
        playerhealthbar.SetMaxHealth(maxPlayerHealth);
    }


    // Update is called once per frame
    void Update()
    {
        // main attack
        if (Time.time >= nextAttack)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttack = Time.time + 1f / attackRate;
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && attackBoostTotal>0)
        {
            attackBoostTotal--;
            abNum.text = "" + attackBoostTotal;
            attackBoost(abstrengthMult, abduration);
        }

    }


    // player health use this to get componets for enemy that do damage.
    // PlayertakeDamage is called whenever the character is attacked
    //-------------------------------------------------------------------------------------------------------------
    public void PlayertakeDamage(int damage)
    {
        // player taking damage from enimes and updates the health bar
        //character health is reduced by damage
        currentPlayerHealth -= damage;
        audioSrc.PlayOneShot(damageClip);
        playerhealthbar.SetHealth(currentPlayerHealth);
        Debug.Log(currentPlayerHealth);

        //If the health reaches 0 call PlayerDeath()
        if (currentPlayerHealth <= 0 && !playerDeath)
        {
            playerDeath = true;
            // if 0 plyer dies and does some death stuff.
            PlayerDeath();

        }
    }

    // TakeDotDamage is called whenever the character is attacked with a move that damages over time
    //------------------------------------------------------------------------------------------------------------
    public void TakeDotDamage(int damage, int duration)
    {
        // player can take DOT damage overtime
        //Call to timer
        StartCoroutine(waiter(damage, duration));
    }

    //TakeDotDamage() Timer
    //-----------------------------------------------------------------------------------------------------------
    IEnumerator waiter(int damage, int duration)
    {
        for (int i = 0; i < duration; i++)
        {
            //character health is reduced by damage
            currentPlayerHealth -= damage;
            audioSrc.PlayOneShot(damageClip);
            // animator.SetTrigger("isAttacked"); // If enemy should display a animation for taking DoT damage

            Debug.Log("DotDamage Health");
            Debug.Log(currentPlayerHealth);

            //If the health reaches 0 call PlayerDeath()
            if (currentPlayerHealth <= 0 && !playerDeath)
            {
                playerDeath = true;
                PlayerDeath();
                yield break;
            }
            yield return new WaitForSeconds(1);
        }
    }


    // player health use this to get componets for enemy that do damage.
    //-----------------------------------------------------------------------------------------------------------
    public void PlayerReceiveHealth(int health)
    {
        // player be able to pick up objects that heal him
         audioSrc.PlayOneShot(healthClip);
        if (currentPlayerHealth < maxPlayerHealth)
        {
            
           currentPlayerHealth += health;

            playerhealthbar.SetHealth(currentPlayerHealth);
            Debug.Log(currentPlayerHealth);

        }
    }

    //Playerdeath gets called whenever the health is less than or equal to 0 
    //--------------------------------------------------------------------------------------------------------
    void PlayerDeath()
    {
        Debug.Log("Remove Totem "+totem);
        if (totem == 0)
        {
          // animation for player death
            audioSrc.PlayOneShot(deathClip);
            animator.SetBool("isDying", true);
            StartCoroutine(deathAnimation());
        }
        else
        {
            audioSrc.PlayOneShot(healthClip);
            playerDeath = false;
            totem--;
            currentPlayerHealth = maxPlayerHealth;
            playerhealthbar.SetHealth(currentPlayerHealth);
            
        }
    }

    public void attackBoost(int strengthMult, int duration)
    {
        StartCoroutine(ABTimer(strengthMult, duration));
    }
    IEnumerator ABTimer(int strengthMult, int duration)
    {
        float tempD = attackDamage;

        //Increase the enemies' attack
        attackDamage = (int)(attackDamage * strengthMult);

        for (int i = 0; i < duration; i++)
        {
            yield return new WaitForSeconds(1);
        }

        //return the attack to normal
        attackDamage = (int)tempD;
    }
    public void addAB()
    {
        attackBoostTotal++;
        abPanel.SetActive(true);
        abNum.text = "" + attackBoostTotal;
    }
    public void addTotem()
    {
        totem++;
        totemPanel.SetActive(true);
        totemNum.text = "" + totem;
    }

    //Timer for death animation
    //--------------------------------------------------------------------------------------------------------
    IEnumerator deathAnimation()
    {

        yield return new WaitForSeconds(.5f);
        Time.timeScale = 0;

    }

    //Attack is called whenever the player is close to the enemy and its attack hits
    //--------------------------------------------------------------------------------------------------------
    void Attack()
    {
        //play an attack animations
        animator.SetTrigger("Attack");

        //detect enmies in range
        Collider2D[] hitEnemiesright = Physics2D.OverlapCircleAll(attackpointright.position, attackRange, ~enemyLayers);
        Collider2D[] hitEnemiesleft = Physics2D.OverlapCircleAll(attackpointleft.position, attackRange, ~enemyLayers);
        Collider2D[] hitEnemiesup = Physics2D.OverlapCircleAll(attackpointup.position, attackRange, ~enemyLayers);
        Collider2D[] hitEnemiesdown = Physics2D.OverlapCircleAll(attackpointdown.position, attackRange, ~enemyLayers);


        //show the damage dealt
        if (moveDirection.x >= 0.1)
        {
            foreach (Collider2D enemy in hitEnemiesright)
            {
                //deal damage to any enemies in the right side of the attack
                Debug.Log("right");
                if(enemy.CompareTag("Boss"))
                {
                    enemy.GetComponent<BossFIght>().TakeDamage(attackDamage);
                    break;
                }
                else
                {
                enemy.GetComponent<EnemyCombat>().TakeDamage(attackDamage);
                    break;
                }
                break;
            }
        }
        if (moveDirection.x <= -0.1)
        {
            foreach (Collider2D enemy in hitEnemiesleft)
            {

                if (enemy.CompareTag("Boss"))
                {
                    enemy.GetComponent<BossFIght>().TakeDamage(attackDamage);
                    break;
                }
                else
                {
                    enemy.GetComponent<EnemyCombat>().TakeDamage(attackDamage);
                    break;
                }
                break;
            }
        }
        if (moveDirection.y >= 0.1)
        {
            foreach (Collider2D enemy in hitEnemiesup)
            {
                Debug.Log("up");
                if (enemy.CompareTag("Boss"))
                {
                    enemy.GetComponent<BossFIght>().TakeDamage(attackDamage);
                    break;
                }
                else
                {
                    enemy.GetComponent<EnemyCombat>().TakeDamage(attackDamage);
                    break;
                }
                break;
            }
        }
        if (moveDirection.y >= -0.1)
        {
            foreach (Collider2D enemy in hitEnemiesdown)
            {
                Debug.Log("down");
                if (enemy.CompareTag("Boss"))
                {
                    enemy.GetComponent<BossFIght>().TakeDamage(attackDamage);
                    break;
                }
                else
                {
                    enemy.GetComponent<EnemyCombat>().TakeDamage(attackDamage);
                    break;
                }
                break;
            }
        }
        if(moveDirection.x == 0 && moveDirection.y == 0)
        {
            foreach (Collider2D enemy in hitEnemiesright)
            {
                if (enemy.CompareTag("Boss"))
                {
                    enemy.GetComponent<BossFIght>().TakeDamage(attackDamage);
                    break;
                }
                else
                {
                    enemy.GetComponent<EnemyCombat>().TakeDamage(attackDamage);
                    break;
                }
                break;
            }
        }

    }

    //Adds a coin to player wealth
    //--------------------------------------------------------------------------------------------------------
    public void AddCoins(int value)
    {
        audioSrc.PlayOneShot(coinClip);
        wealth += value;
        currencyUI.text = "" + wealth;
        Debug.Log(wealth);
    }

    //Removes a coin to player wealth
    //--------------------------------------------------------------------------------------------------------
    public void removeCoins(int cost)
    {
        audioSrc.PlayOneShot(coinClip);
        wealth -= cost;
        currencyUI.text = "" + wealth;
        Debug.Log(wealth);
    }

    //Draws the attack range in the scene view
    //--------------------------------------------------------------------------------------------------------
    private void OnDrawGizmosSelected()
    {

        if (attackpointright == null)
       
            return;
            Gizmos.DrawWireSphere(attackpointright.position, attackRange);
        

        if (attackpointleft == null)
        
            return;
            Gizmos.DrawWireSphere(attackpointleft.position, attackRange);
        
        if (attackpointup == null)
        
            return;
            Gizmos.DrawWireSphere(attackpointup.position, attackRange);
         
        if (attackpointdown == null) 
             return;
            Gizmos.DrawWireSphere(attackpointdown.position, attackRange);
        
    }

    //Sets the last direction the player attacked
    //--------------------------------------------------------------------------------------------------------
    public void setDirection(float moveDirectionX, float moveDirectionY)
    {
        moveDirection = new Vector2(moveDirectionX, moveDirectionY).normalized;
    }
}
