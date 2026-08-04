using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public Transform Target;

    // Update is called once per frame
    void Update()
    {
    }

    private void Start()
    {
        transform.RotateAround(Target.position, Vector3.forward, 90 * Time.deltaTime);
    }
}
