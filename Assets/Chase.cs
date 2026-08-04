using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{
    public Transform Player;

    private NavMeshAgent Agent;
         
    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        // Agent.SetDestination(Player.position);
    }

    // Update is called once per frame
    void Update()
    {
        // Agent.destination = Player.position;
    }
    

}
