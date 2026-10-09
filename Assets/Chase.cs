using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{
    public Transform Player;

    private NavMeshAgent Agent;
    public Animator animator;
   
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Agent = GetComponent<NavMeshAgent>();
            Agent.SetDestination(Player.position);
            animator.SetTrigger("withInRange");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Agent.SetDestination(Player.position);
            animator.SetTrigger("withInRange");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            animator.SetTrigger("outOfRange");
            Agent.ResetPath();

        }
    }

}
