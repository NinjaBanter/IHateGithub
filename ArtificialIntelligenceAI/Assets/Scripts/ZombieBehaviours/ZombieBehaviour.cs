using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehaviour : MonoBehaviour
{
    public float wanderRadius;
    public float wanderTimer;


    public List<GameObject> myList = new List<GameObject>();

    public GameObject alertGameObject;
    public GameObject currentTarget;

    public Animation anim;

    public float visableHumans;

  //  private Vector3 gizmoNewPos;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;

    float shortestDistance;
    float deathDistance = 2f;


    public string zombieState;

    // Use this for initialization
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        zombieState = "Wander";
        anim = gameObject.GetComponent<Animation>();
        anim.Play("ZombieBirth");
        shortestDistance = 500f;

        wanderTimer = Random.Range(1, 3);
        wanderRadius = Random.Range(15, 40);
        

      //  wanderTimer = 0;
      //  wanderRadius = 0;

        timer = wanderTimer;
        alertGameObject.transform.localScale = new Vector3(30, 1, 30);
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * STATES ARE : 
         * WANDER
         * ALERT
         * FULL
         * 
         * */


        timer += Time.deltaTime;
        // Debug.Log("Shortest distance is " + shortestDistance);


        // WANDER STATE --------- WANDER STATE --------- WANDER STATE --------- WANDER STATE --------- WANDER STATE ---------WANDER STATE ---------WANDER STATE ---------
        if (zombieState == "Wander")
        {
            agent.speed = 3.5f;


            if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }
         }

        // ALERT STATE ------------ // ALERT STATE ------------ // ALERT STATE ------------ // ALERT STATE ------------ // ALERT STATE ------------ // ALERT STATE ------------ // ALERT STATE ------------
        if (zombieState == "Alert")
        {

            agent.speed = 10f;

            if(myList.Count == 0)
            {
                zombieState = "Wander";
                //return;
            }

            //foreach(GameObject i in myList)
            for (int i = 0; i < myList.Count; i++)
            {

                GameObject target = myList[i];
                if (myList[i] != null)
                {




                    // float distance = Vector3.Distance(this.gameObject.transform.position, i.transform.position);
                    float distance = Vector3.Distance(this.gameObject.transform.position, target.transform.position);

                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        // currentTarget = i;  
                        currentTarget = target; //; 
                    }
                }
            }

            if(currentTarget != null)
            {
                Vector3 newPos = currentTarget.transform.position;
                agent.SetDestination(newPos);

                HumanBehaviour theHumanBehaviour = currentTarget.GetComponent<HumanBehaviour>(); //

                float distance = Vector3.Distance(this.gameObject.transform.position, currentTarget.transform.position);
                if(distance < deathDistance)
                {
                    if (theHumanBehaviour.isDead == false)
                    {
                        zombieState = "Full";
                         currentTarget.GetComponent<HumanBehaviour>().humanState = "Dead";
                        theHumanBehaviour.humanState = "Dead";
                        Debug.Log("Death to all humans");//Code to kill humans
                        myList.Remove(currentTarget);
                    }
                }
            }




        }


        if(zombieState == "Full")
        {
            myList.Clear();
            Debug.Log("I am Full");

        }
    }




    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }


    void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.blue;
        UnityEditor.Handles.DrawWireDisc(this.transform.position, this.transform.up, wanderRadius);
    }
}

