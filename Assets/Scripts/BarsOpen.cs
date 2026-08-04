using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class BarsOpen : MonoBehaviour
{
    public Animator animator;
   void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wrench"))
        {
            animator.SetTrigger("Open");
        }
    }
}
