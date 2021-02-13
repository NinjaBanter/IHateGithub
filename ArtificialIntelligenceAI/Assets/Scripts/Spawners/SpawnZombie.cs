using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombie : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject myZombie;
    //public GameObject[] zombies;

    public List<GameObject> zombies = new List<GameObject>();

    public Transform playArea;

    public int zombieAmount;
    public float spawnCollisionCheckRadius;


    // This script will simply instantiate the Prefab when the game starts.
    void Awake()
    {

       // zombies = new GameObject[zombieAmount];
        for(int i = 0; i < zombieAmount; i++)
        {
            Vector3 position = new Vector3(UnityEngine.Random.Range(-40.0f, 40.0f), 2, UnityEngine.Random.Range(-40.0f, 40.0f));

            if (!Physics.CheckSphere(position, spawnCollisionCheckRadius))
            {
                GameObject clone = (GameObject)Instantiate(myZombie, position, Quaternion.identity);
                //zombies[i] = clone;
                zombies.Add(clone);
            }
            else
            {
                i--;
            }
        }

    }

}
