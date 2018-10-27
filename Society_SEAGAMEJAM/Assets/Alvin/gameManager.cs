using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public Hashtable values = new Hashtable();
    public Hashtable gamevalues = new Hashtable();
    
    private GameObject textdummy;

    public GameObject[] opportunityRepo;
    public GameObject[] responsibilityRepo;
    public GameObject[] despacitoRepo;

    public GameObject opportunityGameObj;
    public GameObject gridGameObjResponsibility;
    public GameObject gridGameObjProblems;

    public GameObject warningTextObj;

    public int age = 0;

    public float timeToAge = 1.0f;
    public List<string> disasterTriggers = new List<string>() { "10", "20", "30", "40" };
    public int maxDisaster = 5;
    private int disasterCount = 0;

    private IEnumerator coroutine;

    public Image sliderObj;
    public Image sliderObjBase;

    void Awake()
    {
        values.Add("Dream", 0);
        values.Add("DreamGoal", 25000);
        changeText("Dream", values["Dream"].ToString());
        changeText("DreamGoal", values["DreamGoal"].ToString());

        values.Add("Money", 50);
        changeText("Money", "$$$ : " + values["Money"].ToString());
        values.Add("Health", 0);
        values.Add("HealthCap", 50);
        changeText("Health", "Age : " + values["Health"].ToString() + "/" + values["HealthCap"].ToString());

        values.Add("JobTitle", "Dishwasher");
        values.Add("Salary", 100);
        values.Add("JobFluff", "You wash dishes for a living, it aint much, but it puts food in your belly lmao. So unless you want to starve, move those hands"+ "\n\nSalary : " + values["Salary"].ToString());

        gamevalues.Add("WorkBuff", 0);
        gamevalues.Add("WorkModifierBuff", 0);

        values.Add("RespoCount", 0);
        changeText("RespoCount", "You have "+ values["RespoCount"].ToString() + " responsibilities waiting");

        coroutine = AgeUp(timeToAge);
        StartCoroutine(coroutine);
    }

    void updateDreamBar()
    {
        float newVal = (float.Parse(values["Dream"].ToString()) / float.Parse(values["DreamGoal"].ToString()));
        if(newVal == 0)
        {
            newVal = 0.01f;
        }
        sliderObj.fillAmount = newVal;
        sliderObjBase.fillAmount = 1.0f - newVal;
    }

    public void criticalRespo()
    {
        //textdummy = GameObject.Find("RespoCountText");
        //textdummy.GetComponent<Text>().text = "CRITICAL";
        warningTextObj.SetActive(true);
    }

    public void removeCriticalRespo()
    {
        //textdummy = GameObject.Find("RespoCountText");
        //textdummy.GetComponent<Text>().text = "CRITICAL";
        warningTextObj.SetActive(false);
    }

    public void modifyValue(string valueName, int value)
    {
        //Debug.Log(valueName);
        //Debug.Log(values[valueName]);
        values[valueName] = int.Parse(values[valueName].ToString()) + value;
        changeText(valueName, values[valueName].ToString());
    }

    public void applyBuff(string buffname, int value)
    {
        gamevalues[buffname] = int.Parse(gamevalues[buffname].ToString()) + value;
        changeText(buffname, gamevalues[buffname].ToString());
    }

    public void setValue(string valueName, int value)
    {
        values[valueName] = value;
        changeText(valueName, values[valueName].ToString());
    }


    public void changeText(string textName, string theString)
    {
        if(textName == "Dream")
        {
            updateDreamBar();
        }

        textdummy = GameObject.Find(textName+"Text");
        textdummy.GetComponent<Text>().text = theString;
    }

    public void spawnOpportunity(Vector3 locationTrans, GameObject that)
    {
        GameObject newGameObject = Instantiate(opportunityRepo[Random.Range(0, opportunityRepo.Length)], locationTrans, Quaternion.identity);
        newGameObject.transform.SetParent(opportunityGameObj.transform);
        newGameObject.GetComponent<clickToDismiss>().opButton = that;
    }

    public void giveResponsibility(int index)
    {
        GameObject newGameObject = Instantiate(responsibilityRepo[index], new Vector3(), Quaternion.identity);
        newGameObject.transform.SetParent(gridGameObjResponsibility.transform);
    }

    public void giveProblems(int index)
    {
        GameObject newGameObject = Instantiate(despacitoRepo[index], new Vector3(), Quaternion.identity);
        newGameObject.transform.SetParent(gridGameObjProblems.transform);
    }

    public void gameOver()
    {
        Debug.Log("LOSE");
    }

    public void victory()
    {
        Debug.Log("WIN");
    }

    private IEnumerator AgeUp(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            values["Health"] = int.Parse(values["Health"].ToString())+1;
            if(disasterTriggers.Contains(values["Health"].ToString()))
            {
                if(disasterCount < maxDisaster)
                {
                    giveProblems(Random.Range(0, despacitoRepo.Length));
                    disasterCount++;
                }
            }

            changeText("Health", "Age : " + values["Health"].ToString() + "/" + values["HealthCap"].ToString());

            if (int.Parse(values["Health"].ToString()) >= int.Parse(values["HealthCap"].ToString()))
            {
                Debug.Log("You Dea my boiiii");
            }
        }
    }
}
