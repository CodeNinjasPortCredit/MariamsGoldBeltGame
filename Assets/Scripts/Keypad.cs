using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    private string Answer = "3749";

    [SerializeField] private TMP_InputField Ans;
    [SerializeField] private Animator Door;
    [SerializeField] private int maxCharacters = 4;

    void Start()
    {
    }

    public void Number(int number)
    {
        if (Ans.text.Length < 4)
        {
            Ans.text += number.ToString();
        }
    }

    public void Execute()
    {
        if (Ans.text == Answer)
        {
            Ans.text = "Correct";
            Door.SetBool("Open", true);
            gameObject.SetActive(false);
            //Door.rbDoor.AddRelativeTorque(new Vector3(0, 0, 20f));
        }
        else
        {
            Ans.text = "Incorrect";
            Invoke("ResetAns", 1f); 
        }
    }

    void ResetAns()
    {
        Ans.text = "";
    }
}
