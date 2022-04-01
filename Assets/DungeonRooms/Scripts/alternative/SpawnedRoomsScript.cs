using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedRoomsScript : MonoBehaviour
{
     
    public GameObject[] spawnedRooms;
    public GameObject[] spawnedDoors;
    GameObject[] oneDoorArray;
    
    public List<GameObject> oneDoorRooms = new List<GameObject>();

    public LevelSizeScript levelSize;
    private int randomNumber1;
    private int randomNumber2;


    public GameObject[] bossRooms;
    public GameObject[] storeRooms;
   
   

    public int numberOfTotalRooms;

    void Start(){
        numberOfTotalRooms = 0;

        StartCoroutine(ExecuteAfterTime(4));

    }


    void Update(){

        spawnedRooms = GameObject.FindGameObjectsWithTag("Room");
        spawnedDoors = GameObject.FindGameObjectsWithTag("DoorParent");
        numberOfTotalRooms = spawnedRooms.Length + bossRooms.Length + storeRooms.Length;

        

    }//end update 


    


    
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        
        
        

        for(int i = 0; i < spawnedDoors.Length; i++){

            if(spawnedDoors[i].transform.childCount == 1){
                //if the doorparent object only has one child, that means the room only has one door
                //only having one door means the room is a candidate to be a special room
                //if special rooms have more than one door that could lead to rooms beyond being
                // "locked away", or otherwise be inaccessible

                oneDoorRooms.Add(spawnedDoors[i].transform.parent.gameObject);
            }
        }//end for

        oneDoorArray = oneDoorRooms.ToArray();


        if (oneDoorArray.Length >= 2){
            randomNumber1 = Random.Range(0,oneDoorArray.Length - 1);
            randomNumber2 = Random.Range(0,oneDoorArray.Length - 1);
        }
        while(randomNumber1 == randomNumber2){
            randomNumber2 = Random.Range(0,oneDoorArray.Length - 1);
        }

        setTag(randomNumber1, randomNumber2);

    }//end enumerator

    


    void setTag(int rn1, int rn2)
    {
        
        setBossTag(oneDoorArray[rn1]);
        setStoreTag(oneDoorArray[rn2]);

    }//end tag
    


    void setBossTag(GameObject room){

        if(GameObject.FindGameObjectsWithTag("BossRoom").Length == 0){

                

                room.tag = "BossRoom";
                bossRooms = GameObject.FindGameObjectsWithTag("BossRoom");
        }//end if
        

    }//end boss

    void setStoreTag(GameObject room){

        if(GameObject.FindGameObjectsWithTag("StoreRoom").Length == 0){


                room.tag = "StoreRoom";
                storeRooms = GameObject.FindGameObjectsWithTag("StoreRoom");
            }//if
        

    }// end store


}// end class
