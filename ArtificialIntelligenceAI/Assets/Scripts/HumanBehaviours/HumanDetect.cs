using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanDetect : MonoBehaviour
{

    private GameObject theHuman;
    private HumanBehaviour theHumanBehaviour;
    // Start is called before the first frame update
    void OnEnable()
    {
        theHuman = transform.parent.gameObject;
        theHumanBehaviour = theHuman.GetComponent<HumanBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Zombie")
        {
            theHumanBehaviour.humanState = "Evade";
            //  theHumanBehaviour.currentTarget = other.transform.gameObject;
            theHumanBehaviour.zombieList.Add(other.transform.gameObject);
            //theZombie.GetComponent<ZombieBehaviour>().currentTarget = other.transform.gameObject;
            //theZombie.GetComponent<ZombieBehaviour>().myList(add)
            // Debug.Log("Working");
        }
    }


    private void OnTriggerExit(Collider other)
    {
        theHumanBehaviour.zombieList.Remove(other.transform.gameObject);
    }


}
