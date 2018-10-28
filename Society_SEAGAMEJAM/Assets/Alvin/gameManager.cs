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

    public GameObject[] despacitoEventRepo;

    public GameObject opportunityGameObj;
    public GameObject gridGameObjResponsibility;
    public GameObject gridGameObjProblems;

    public GameObject warningTextObj;

    public Image jobImage;

    public Sprite Job1Img;
    public Sprite Job2Img;
    public Sprite Job3Img;

    public Image characterImage;
    public Sprite CharacterJob1Img;
    public Sprite CharacterJob2Img;
    public Sprite CharacterJob3Img;

    public int age = 0;

    public float timeToAge = 1.0f;
    public List<string> disasterTriggers = new List<string>() { "10", "20", "30", "40" };
    public int maxDisaster = 5;
    private int disasterCount = 0;

    private IEnumerator coroutine;

    public Image sliderObj;
    public Image sliderObjBase;

    void Update()
    {
        changeText("Salary", values["Salary"].ToString());
        changeText("Health", values["Health"].ToString());
    }

    void Awake()
    {
        values.Add("Dream", 0);
        values.Add("DreamGoal", 25000);
        changeText("Dream", values["Dream"].ToString());
        changeText("DreamGoal", values["DreamGoal"].ToString());

        values.Add("Money", 50000);
        changeText("Money", values["Money"].ToString());
        values.Add("Health", 18);
        values.Add("HealthCap", 80);
        changeText("Health", values["Health"].ToString() + "/" + values["HealthCap"].ToString());

        values.Add("JobTitle", "NEET");
        values.Add("Salary", 0);
        values.Add("jobGoal", 100);
        values.Add("JobFluff", "Wasting money, wasting time");
        changeText("Salary", values["Salary"].ToString());


        values.Add("ProblemsCost", 0);
        values.Add("insuranceReduction", 0);

        values.Add("WorkBuff", 0);
        values.Add("WorkModifierBuff", 0);

        values.Add("DreamsBuff", 0);
        values.Add("OpportunityBuff", 0);
        
        values.Add("RespoCount", 0);
        changeText("RespoCount", "Active : " + values["RespoCount"].ToString());

        values.Add("ProblemCount", 0);
        changeText("ProblemCount", "Total : " + values["ProblemCount"].ToString());

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

        if (valueName == "HealthCap")
        {
            valueName = "Health";
        }

        changeText(valueName, values[valueName].ToString());
    }

    public void applyBuff(string buffname, int value)
    {
        values[buffname] = int.Parse(values[buffname].ToString()) + value;
        //changeText(buffname, gamevalues[buffname].ToString());
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

        if (textName == "Salary")
        {
            theString = "$" + theString;
        }

        textdummy = GameObject.Find(textName+"Text");

        if (textName == "Health")
        {
            theString = values["Health"].ToString() + "/" + values["HealthCap"].ToString();
        }

        textdummy.GetComponent<Text>().text = theString;
    }

    public void spawnOpportunity(GameObject that)
    {
        GameObject stuffToInstantiate = null;
        while (stuffToInstantiate == null)
        {
            stuffToInstantiate = opportunityRepo[Random.Range(0, opportunityRepo.Length)];
        }

        GameObject newGameObject = Instantiate(stuffToInstantiate, new Vector3(0, -178, 0), Quaternion.identity);
        newGameObject.transform.SetParent(opportunityGameObj.transform,false);
        newGameObject.GetComponent<clickToDismiss>().opButton = that;
    }

    public void spawnProblems()
    {
        int index = 0;
        GameObject stuffToInstantiate = null;
        while (stuffToInstantiate == null)
        {
            index = Random.Range(0, despacitoRepo.Length);
            stuffToInstantiate = despacitoRepo[index];
        }

        GameObject newGameObject = Instantiate(stuffToInstantiate, new Vector3(), Quaternion.identity);
        newGameObject.transform.SetParent(gridGameObjProblems.transform);

        GameObject newGameObject2 = Instantiate(despacitoEventRepo[index], new Vector3(0, -178, 0), Quaternion.identity);
        newGameObject2.transform.SetParent(opportunityGameObj.transform, false);
        newGameObject2.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -178, 0);

        values["ProblemCount"] = int.Parse(values["ProblemCount"].ToString()) + 1;
        changeText("ProblemCount", "Total : " + values["ProblemCount"].ToString());
    }

    public void giveResponsibility(int index)
    {
        GameObject newGameObject = Instantiate(responsibilityRepo[index], new Vector3(), Quaternion.identity);
        newGameObject.transform.SetParent(gridGameObjResponsibility.transform);

        values["RespoCount"] = int.Parse(values["RespoCount"].ToString()) + 1;
        changeText("RespoCount", "Active : " + values["RespoCount"].ToString());
    }

    public void changeJob(int index)
    {
        switch(index)
        {
            case 1:
                values["JobTitle"] = "Waiter";
                values["Salary"] = 100;
                values["jobGoal"] = 25;
                values["JobFluff"] = "Not a glorious job, but it pays";
                jobImage.sprite = Job1Img;
                characterImage.sprite = CharacterJob1Img;
                break;
            case 2:
                values["JobTitle"] = "Office";
                values["Salary"] = 500;
                values["jobGoal"] = 50;
                values["JobFluff"] = "Pencil pushers get a decent pay!";
                jobImage.sprite = Job2Img;
                characterImage.sprite = CharacterJob2Img;
                break;
            case 3:
                values["JobTitle"] = "Entrepreneur";
                values["Salary"] = 1000;
                values["jobGoal"] = 100;
                values["JobFluff"] = "This is it, as big as you are gonna get";
                jobImage.sprite = Job3Img;
                characterImage.sprite = CharacterJob3Img;
                break;
        }

        changeText("JobTitle", values["JobTitle"].ToString());
        changeText("JobFluff", values["JobFluff"].ToString());
        changeText("Salary", values["Salary"].ToString());
    }

    public void removeResponsibility()
    {
        values["RespoCount"] = int.Parse(values["RespoCount"].ToString()) - 1;
        changeText("RespoCount", "Active : " + values["RespoCount"].ToString());
    }

    public void giveProblems(int index)
    {
        GameObject newGameObject = Instantiate(despacitoRepo[index], new Vector3(), Quaternion.identity);
        newGameObject.transform.SetParent(gridGameObjProblems.transform);

        GameObject newGameObject2 = Instantiate(despacitoEventRepo[index], new Vector3(), Quaternion.identity);
        newGameObject2.transform.SetParent(opportunityGameObj.transform,false);

        values["ProblemCount"] = int.Parse(values["ProblemCount"].ToString()) + 1;
        changeText("ProblemCount", "Total : " + values["ProblemCount"].ToString());
    }

    public void gameOver()
    {
        Debug.Log("LOSE");
    }

    public void victory()
    {
        Debug.Log("WIN");
    }

    public void removeOpportunities(int index)
    {
        opportunityRepo[index] = null;
    }

    public void removeProblems()
    {
        values["ProblemCount"] = int.Parse(values["ProblemCount"].ToString()) - 1;
        changeText("ProblemCount", "Total : " + values["ProblemCount"].ToString());
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
                    spawnProblems();
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
