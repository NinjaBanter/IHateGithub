﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDetect : MonoBehaviour
{
    private GameObject theZombie;
    private ZombieBehaviour theZombieBehaviour;

    // Start is called before the first frame update
    void OnEnable()
    {
        theZombie = transform.parent.gameObject;
        theZombieBehaviour = theZombie.GetComponent<ZombieBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Human" && other.GetComponent<HumanBehaviour>().isDead == false && theZombie.GetComponent<ZombieBehaviour>().zombieState != "Full")
        {
            Debug.Log("Chonk chonk chonk");
            theZombieBehaviour.zombieState = "Alert";
       //     theZombieBehaviour.currentTarget = other.transform.gameObject;
            theZombieBehaviour.myList.Add(other.transform.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(theZombieBehaviour.zombieState != "Full")

        //theZombie.GetComponent<ZombieBehaviour>().zombieState = "Wander";
        theZombieBehaviour.myList.Remove(other.transform.gameObject);

        if(theZombieBehaviour.myList.Count == 0)
        {
            theZombie.GetComponent<ZombieBehaviour>().zombieState = "Wander";
        }
    }
}
