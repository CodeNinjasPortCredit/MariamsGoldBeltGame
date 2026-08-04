using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LaserController : MonoBehaviour
{
    public GameObject Lasers;
    public float Timer;

    private bool IsLaserOn=true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LaserTimer());
    }

    IEnumerator LaserTimer()
    {
        while (true)
        {
            IsLaserOn = true;
            SetLasers(true);
            yield return new WaitForSeconds(Timer);

            IsLaserOn = false;
            SetLasers(false);
            yield return new WaitForSeconds(Timer);
        }
    }


    // Update is called once per frame
    void SetLasers(bool Active)
    {
        if (Lasers!= null)
            {
                Lasers.SetActive(Active);
            }
    }
}
