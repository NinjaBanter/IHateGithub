using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHuman : MonoBehaviour
{
    public GameObject myHuman;
    // public GameObject[] humans;
    public List<GameObject> humans = new List<GameObject>();


    public int humanAmount;
    public float spawnCollisionCheckRadius;

    void Awake()
    {

        for (int i = 0; i < humanAmount; i++)
        {
            Vector3 position = new Vector3(UnityEngine.Random.Range(-40.0f, 40.0f), 2, UnityEngine.Random.Range(-40.0f, 40.0f));
            if (!Physics.CheckSphere(position, spawnCollisionCheckRadius))
            {
                GameObject clone = (GameObject)Instantiate(myHuman, position, Quaternion.identity);
                humans.Add(clone);
            }
            else
            {
                i--;
            }
        }

    }
}
