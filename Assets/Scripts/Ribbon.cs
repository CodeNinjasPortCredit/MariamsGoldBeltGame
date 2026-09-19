using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ribbon : MonoBehaviour
{
    public Timer timer;

    private Rigidbody rb;
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            timer.endTimer();
            SceneManager.LoadScene("Win");
        }
    }
}
