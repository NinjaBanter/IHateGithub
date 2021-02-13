using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanBehaviour : MonoBehaviour
{

    [SerializeField]
    

    NavMeshAgent _navMeshAgent;
    GameObject _destination;
    public GameObject zombieSpawner;
    public GameObject alertGameObject;
    public List<GameObject> zombieList = new List<GameObject>();
    public string humanState;

    public GameObject newZombie;


    public float alertDistance;
    public float wanderTimer;
    public float wanderRadius;
    public float timer;

    public Animation anim;

    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        zombieSpawner = GameObject.FindGameObjectWithTag("Zspawner");
        anim = gameObject.GetComponent<Animation>();
        alertDistance = 4f;
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        alertGameObject.transform.localScale = new Vector3(10, 1, 10);

        wanderRadius = 10;
        wanderTimer = 2;

        timer = wanderTimer;

        humanState = "Wander";

    }

    private void SetDestination()
    {
        if (_destination != null)
        {
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);
        }
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if (humanState != "Dead")
        {

            if (zombieList.Count != 0 && !isDead)
            {
                humanState = "Evade";
                foreach (GameObject zombie in zombieList)
                {
                    float distance = Vector3.Distance(this.gameObject.transform.position, zombie.transform.position);

                    if (distance < alertDistance)
                    {
                        Vector3 dirToEnemy = transform.position - zombie.transform.position; // Flee steering Behaviour
                        Vector3 newPos = transform.position + dirToEnemy;

                        _navMeshAgent.SetDestination(newPos);
                    }
                }
            }
            else
            {
                humanState = "Wander";
            }


            if (humanState == "Wander")
            {
                _navMeshAgent.speed = 3.5f;


                if (timer >= wanderTimer)
                {
                    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                    _navMeshAgent.SetDestination(newPos);
                    timer = 0;
                }
            }
        }
        /*
        if(isDead == true)
        {
            humanState = "Dead";
        }
        */

        if(humanState == "Dead")
        {
            if(!isDead)
            {
                GameObject clone = (GameObject)Instantiate(newZombie, this.transform.position, Quaternion.identity);
                zombieSpawner.GetComponent<SpawnZombie>().zombies.Add(clone);

                anim.Play("HumanDeath");

                isDead = true;
                Invoke("RemoveObject", 1f);
                //invoke(RemoveObject, 1f);
               
            }
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    public void RemoveObject()
    {
        Destroy(gameObject);
    }
}

