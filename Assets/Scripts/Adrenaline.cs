using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adrenaline : MonoBehaviour
{
    public Transform player;
    public float multiplier;
    public GameObject buffedArrow;
    public float tempS;
    public int tempD;
    public GameObject tempArrow;
    public float duration = 10f;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent;
        tempS = player.GetComponent<PlayerControl>().moveSpeed;
        player.GetComponent<PlayerControl>().moveSpeed = tempS * 2;
        tempD = player.GetComponent<PlayerCombat>().attackDamage;
        player.GetComponent<PlayerCombat>().attackDamage = tempD * 2;
        tempArrow = player.GetComponent<Spell>().arrowfab;
        player.GetComponent<Spell>().arrowfab = buffedArrow;
        StartCoroutine(BuffDuration());
    }

    IEnumerator BuffDuration()
    {
        yield return new WaitForSeconds(duration);
        //Any visual effects for buff turning off can go here
        player.GetComponent<PlayerControl>().moveSpeed = tempS; // Return speed to normal
        player.GetComponent<PlayerCombat>().attackDamage = tempD; // Return damage to normal
        player.GetComponent<Spell>().arrowfab = tempArrow; // Return Arrow to normal
        Destroy(gameObject);
    }
}
