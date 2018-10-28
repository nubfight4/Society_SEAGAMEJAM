using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScriptCustom : MonoBehaviour
{
    public gameManager gameManagerScript;

    public string gameEffectName = "N/A"; //gameEffects that it checks for to increase perclick with
    public string gameEffectNameModifier = "N/A"; //gameEffects that it checks for to increase value with
    public string gameEffectNameTimer = "N/A"; //gameEffects that it checks for to increase decay rate with

    private float gameEffectNameValue = 0;
    private float gameEffectNameTimerValue = 0;

    public int increaseChange = 0;

    public Image sliderObj;
    public Image sliderObjBase;
    public Text textObj;

    public string Title;
    public string fluffText;

    public Text TitleObj;
    public Text fluffTextObj;
    public Text costTextObj;

    public bool countDown;

    public enum type { problem, opportunity, killToProblem, FatalKill, Neutral, Dream, Job, respoDecay};
    public type typeVar;
    public int problemIndex;

    public int currentVal;
    public int goalVal;

    public int[] mainValueGain = { 0 };
    public string[] resourceOnClick = { "Money" };
    public int[] valueChangePerClick = { 0 };
    
    public string[] resourceOnClickDrain = { "N/A" };
    public int[] valueChangePerClickDrain = { 0 };

    public float bleedSpeed = 0.5f;
    public int[] valueDrain = { 0 };

    private IEnumerator coroutine;

    public bool buff;
    public string[] buffEffect = { "N/A" };
    public int[] buffEffectChange = { 0 };

    void Update()
    {
        if (typeVar == type.Job)
        {
            Title = gameManagerScript.values["JobTitle"].ToString();
            fluffText = gameManagerScript.values["JobFluff"].ToString();
            valueChangePerClick[0] = int.Parse(gameManagerScript.values["Salary"].ToString());

            goalVal = int.Parse(gameManagerScript.values["jobGoal"].ToString());
        }
    }

    void Start()
    {
        gameManagerScript = GameObject.Find("_gameManager").GetComponent<gameManager>();

        if (typeVar == type.Job)
        {
            Title = gameManagerScript.values["JobTitle"].ToString();
            fluffText = gameManagerScript.values["JobFluff"].ToString();
            valueChangePerClick[0] = int.Parse(gameManagerScript.values["Salary"].ToString());

            goalVal = int.Parse(gameManagerScript.values["jobGoal"].ToString());
        }

        if (typeVar == type.problem)
        {
            int insuranceReduction = 0;
            insuranceReduction = int.Parse(gameManagerScript.values["insuranceReduction"].ToString());
            valueChangePerClickDrain[0] -= insuranceReduction;
        }
        
        //sliderObj = this.transform.Find("Slider").GetComponent<Slider>();
        //textObj = this.transform.Find("Count").GetComponent<Text>();
        if (TitleObj != null)
            TitleObj.text = Title;

        if (fluffTextObj != null)
            fluffTextObj.text = fluffText;

        if (costTextObj != null)
            costTextObj.text = "Cost : $"+ Mathf.Abs(valueChangePerClickDrain[0]);
        
        changeText();
        changeSlider();

        if (countDown)
        {
            if(gameEffectNameTimerValue != 0)
            {
                bleedSpeed = (float)bleedSpeed * float.Parse(gameManagerScript.values[gameEffectNameTimer].ToString());
            }
            
            coroutine = bleedValue(bleedSpeed);
            StartCoroutine(coroutine);
        }

        if(buff)
        {
            for (int i = 0; i < buffEffect.Length; i++)
                gameManagerScript.applyBuff(buffEffect[i], buffEffectChange[i]);
        }
    }

    void changeSlider()
    {
        if(typeVar != type.Dream && typeVar != type.problem)
        {
            float newVal = ((float)currentVal / (float)goalVal);

            if (newVal == 0)
            {
                newVal = 0.01f;
            }

            sliderObj.fillAmount = newVal;
            sliderObjBase.fillAmount = 1.0f - newVal;
        }
    }

    void changeText()
    {
        if (typeVar != type.Dream && typeVar != type.problem && typeVar != type.killToProblem && typeVar != type.respoDecay)
        {
            textObj.text = currentVal + " / " + goalVal;
        }
    }

    public void clicketyClack()
    {
        if(countDown)
        {
            SoundManagerScript.mInstance.PlaySFX(AudioClipID.SFX_MOUSE_CLICK);
            bool error = false;
            for (int i = 0; i < resourceOnClickDrain.Length; i++)
            {
                if (int.Parse(gameManagerScript.values[resourceOnClickDrain[i]].ToString()) + valueChangePerClickDrain[i] < 0)
                {
                    //give an error
                    error = true;
                }
            }

            if (!error)
            {
                gameManagerScript.removeCriticalRespo();
                AdjustValue();
            }
        }
        else
        {
            int tempGameVal = 0;
            if (gameEffectName != "N/A")
            {
                tempGameVal = int.Parse(gameManagerScript.values[gameEffectName].ToString());
            }

            if (typeVar == type.problem)
            {
                SoundManagerScript.mInstance.PlaySFX(AudioClipID.SFX_MOUSE_CLICK);
                bool error = false;
                for (int i = 0; i < resourceOnClickDrain.Length; i++)
                {
                    if (int.Parse(gameManagerScript.values[resourceOnClickDrain[i]].ToString()) + valueChangePerClickDrain[i] < 0)
                    {
                        //give an error
                        error = true;   
                    }
                }
                
                if (!error)
                {
                    currentVal += increaseChange + tempGameVal;

                    AdjustValue();
                    changeText();
                    changeSlider();
      
                    if (currentVal <= 0)
                    {
                        emptyBar();
                    }
                }
            }
            else if(typeVar == type.opportunity)
            {
                SoundManagerScript.mInstance.PlaySFX(AudioClipID.SFX_MOUSE_CLICK);
                currentVal += increaseChange + tempGameVal;
                if (currentVal >= goalVal)
                {
                    fillOpportunity();
                }
                changeText();
                changeSlider();
            }
            else
            {
                SoundManagerScript.mInstance.PlaySFX(AudioClipID.SFX_CASH);
                bool error = false;
                for (int i = 0; i < resourceOnClickDrain.Length; i++)
                {
                    if (int.Parse(gameManagerScript.values[resourceOnClickDrain[i]].ToString()) + valueChangePerClickDrain[i] < 0)
                    {
                        //give an error
                        error = true;
                    }
                }

                if (!error)
                {
                    if(increaseChange + tempGameVal > 0)
                    {
                        currentVal += increaseChange + tempGameVal;
                    }
                    else
                    {
                        currentVal += 0;
                    }
                    
                    if (currentVal >= goalVal)
                    {
                        fillBar();
                    }
                    changeText();
                    changeSlider();
                }
            }
        }
    }

    public void fillBar()
    {
        currentVal = 0;
        AdjustValue();
    }

    public void fillOpportunity()
    {
        currentVal = 0;
        changeText();
        changeSlider();
        gameManagerScript.spawnOpportunity(gameObject);
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    public void emptyBar()
    {
        if(typeVar == type.killToProblem)
        {
            gameManagerScript.giveProblems(problemIndex); 
        }
        else if(typeVar == type.FatalKill)
        {
            gameManagerScript.gameOver();
        }

        for (int i = 0; i < buffEffect.Length; i++)
            gameManagerScript.applyBuff(buffEffect[i], -1 * buffEffectChange[i]);

        gameManagerScript.removeCriticalRespo();
        if(typeVar == type.problem)
        {
            gameManagerScript.removeProblems();
        }
        else
        {
            gameManagerScript.removeResponsibility();
        }
        
        Destroy(gameObject);
    }

    void AdjustValue()
    {
        int tempGameVal = 0;
        if (gameEffectNameModifier != "N/A")
        {
            tempGameVal = int.Parse(gameManagerScript.values[gameEffectNameModifier].ToString());
        }
        
        if (mainValueGain[0] == 0)
        {
            if (currentVal + valueChangePerClick[0] + tempGameVal > goalVal)
            {
                currentVal = goalVal;
            }
            else
            {
                currentVal += valueChangePerClick[0] + tempGameVal;
            }
        }
        else
        {
            for (int i = 0; i < resourceOnClick.Length; i++)
            {
                if (mainValueGain[i] == 1)
                {
                    //Debug.Log(gameManagerScript.values[resourceOnClick]);
                    gameManagerScript.modifyValue(resourceOnClick[i], valueChangePerClick[i] + tempGameVal);
                }
                else if (mainValueGain[i] == 2)
                {
                    if (currentVal + valueChangePerClick[i] + tempGameVal > goalVal)
                    {
                        currentVal = goalVal;
                    }
                    else
                    {
                        currentVal += valueChangePerClick[i] + tempGameVal;
                    }

                    gameManagerScript.setValue(resourceOnClick[i], currentVal);
                }
            }
        }

        for (int i = 0; i < resourceOnClickDrain.Length; i++)
        {
            if (resourceOnClickDrain[i] != "N/A")
            {
                gameManagerScript.modifyValue(resourceOnClickDrain[i], valueChangePerClickDrain[i]);
            }
        }
        //Debug.Log(gameManagerScript.values[resourceOnClick]);
    }

    private IEnumerator bleedValue(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            currentVal = currentVal - valueDrain[0];
            changeText();
            changeSlider();
            if (currentVal <= (float) goalVal/4.0f)
            {
                gameManagerScript.criticalRespo();
            }

            if (currentVal <= 0)
            {
                emptyBar();
            }
        }
    }
}
