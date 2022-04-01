using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    

    //public SpawnedRoomsScript getBool;
    bool isColliding = false;
    public bool doorsDestroyed = false;

    void OnTriggerEnter2D(Collider2D other){

        isColliding = true;


    }


    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
 
        // Code to execute after the delay
        if (isColliding == false){
            //getBool.doorsDestroyed = true;
            Destroy(gameObject);
        }
        
    }
        
    void Start(){

        //StartCoroutine(ExecuteAfterTime(2));

    }

    
}
