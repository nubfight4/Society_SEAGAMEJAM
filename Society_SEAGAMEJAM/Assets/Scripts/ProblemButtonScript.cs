using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ProblemType
{
    NONE = 0,
    PROBLEM01 = 1,
    PROBLEM02 = 2,
    PROBLEM03 = 3,
    PROBLEM04 = 4,
    PROBLEM05 = 5,
    PROBLEM06 = 6,
    PROBLEM07 = 7,
    PROBLEM08 = 8,
}

public class ProblemButtonScript : MonoBehaviour {

    //Problem Panel
    public ProblemType currentProblem;
    string problemName;
    float problemDebuff;
    float problemCost;
    public Text problemNameText;
    public Text problemDebuffText;
    public Text problemCostText;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        #region ProblemPanel enums
        if (currentProblem == ProblemType.NONE)
        {
            problemName = "NONE";
            problemDebuff = 0;
            problemCost = 0;
        }
        else if (currentProblem == ProblemType.PROBLEM01)
        {
            problemName = "Problem One";
            problemDebuff = 1;
            problemCost = 1;
        }
        else if (currentProblem == ProblemType.PROBLEM02)
        {
            problemName = "Problem Two";
            problemDebuff = 2;
            problemCost = 2;
        }
        else if (currentProblem == ProblemType.PROBLEM03)
        {
            problemName = "Problem Three";
            problemDebuff = 3;
            problemCost = 3;
        }
        else if (currentProblem == ProblemType.PROBLEM04)
        {
            problemName = "Problem Four";
            problemDebuff = 4;
            problemCost = 4;
        }
        else if (currentProblem == ProblemType.PROBLEM05)
        {
            problemName = "Problem Five";
            problemDebuff = 5;
            problemCost = 5;
        }
        else if (currentProblem == ProblemType.PROBLEM06)
        {
            problemName = "Problem Six";
            problemDebuff = 6;
            problemCost = 6;
        }
        else if (currentProblem == ProblemType.PROBLEM07)
        {
            problemName = "Problem Seven";
            problemDebuff = 7;
            problemCost = 7;
        }
        else if (currentProblem == ProblemType.PROBLEM08)
        {
            problemName = "Problem Eight";
            problemDebuff = 8;
            problemCost = 8;
        }
        #endregion

        problemNameText.text = "Problem: " + problemName;
        problemDebuffText.text = problemDebuff.ToString() + " Max Age";
        problemCostText.text = "Cost: " + problemCost.ToString();
    }
}
