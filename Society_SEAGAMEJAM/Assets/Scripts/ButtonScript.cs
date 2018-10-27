using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

enum JobType
{
    WAITER = 0,
    BUSINESSMAN = 1,
    CEO = 2,
    TOTAL = 9001
}

public class ButtonScript : MonoBehaviour {
    #region Variables
    //Player Variables
    public float age;
    public float maxAge;
    public float ageHardCap;
    public float money;

    //Dream Variables
    public float currentDreamValue;
    public float maxDreamValue;
    float moneyRequiredForInvestDream = 10.0f;
    float investDreamPower = 25.0f;

    //Work Variables
    JobType currentJob;
    bool jobOne = true;
    bool jobTwo = false;
    string jobDescription;
    float currentWorkClicks = 0.0f;
    float WorkClicksNeeded;
    float salary;

    //UI stuff
    public Text moneyText;
    public Text healthText;
    public Text dreamText;
    public Text workButtonText;
    public Image dreamBar;

    //One-off example Button
    bool problemOneExist = false;
    bool problemOneSolved = false;
    float problemOneValue = 5.0f;
    float moneyRequiredProblemOne = 10.0f;
    public GameObject problemOneButton;
    #endregion

    #region Timers Variable

    //Age Timer
    public float ageTimer;
    float ageTimeLeft;

    //Limited Timers
    float limitedTimer;
    float limitedTimeLeft;

    //Upkeep Timers
    float upkeepTimer;
    float upkeepTimeLeft;

    #endregion

    private void Start()
    {
        currentJob = JobType.WAITER;
        ageTimeLeft = ageTimer;
    }

    private void Update()
    {

        #region Work Function
        if(currentJob == JobType.WAITER)
        {
            jobDescription = "Waiter";
            WorkClicksNeeded = 5.0f;
            salary = 1.0f;
        }
        else if(currentJob == JobType.BUSINESSMAN)
        {
            jobDescription = "Businessman";
            WorkClicksNeeded = 10.0f;
            salary = 10.0f;
        }

        if(currentWorkClicks >= WorkClicksNeeded)
        {
            currentWorkClicks = 0.0f;
            money++;
        }
        #endregion

        #region One-off Problem example
        if(problemOneSolved || age < 5)
        {
            problemOneButton.SetActive(false);
        }

        if(age >= 5 && !problemOneSolved && !problemOneExist)
        {
            problemOneExist = true;
            maxAge -= problemOneValue;
            problemOneButton.SetActive(true);
        }
        #endregion

        #region UI stuff

        moneyText.text = money.ToString();
        healthText.text = age.ToString() + " / " + maxAge.ToString();
        dreamText.text = currentDreamValue.ToString() + " / " + maxDreamValue.ToString();
        workButtonText.text = jobDescription + "  " + currentWorkClicks.ToString() + " / " + WorkClicksNeeded.ToString();
        dreamBar.fillAmount = currentDreamValue / maxDreamValue;
        
        #endregion

        #region Health Function
        if(ageHardCap <= maxAge)
        {
            maxAge = ageHardCap;
        }
        ageTimeLeft -= Time.deltaTime;
        if (ageTimeLeft <= 0.0f)
        {
            age += 1.0f;
            ageTimeLeft = ageTimer;
        }
        #endregion

        #region Victory/Defeat Conditions
        if (age >= maxAge)
        {
            SceneManager.LoadScene("Defeat");
        }

        if(currentDreamValue >= maxDreamValue)
        {
            SceneManager.LoadScene("Victory");
        }
        #endregion
    }

    public void DreamButtonFunction()
    {
        currentDreamValue += 1.0f;
    }

    public void InvestDreamButtonFunction()
    {
        if(money >=10.0f)
        {
            money -= moneyRequiredForInvestDream;
            currentDreamValue += investDreamPower;
        }
    }

    public void WorkButtonFunction()
    {
        if (currentWorkClicks < WorkClicksNeeded)
        {
            currentWorkClicks++;
        }
    }

    public void ProblemOneButtonFunction()
    {
        if(money >= moneyRequiredProblemOne)
        {
            money -= moneyRequiredProblemOne;
            maxAge += problemOneValue;
            problemOneSolved = true;
            problemOneExist = false;
        }
    }
}

