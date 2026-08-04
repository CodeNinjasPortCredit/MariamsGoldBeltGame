using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLockExample : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Press Escape to unlock and show the cursor
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.None))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
