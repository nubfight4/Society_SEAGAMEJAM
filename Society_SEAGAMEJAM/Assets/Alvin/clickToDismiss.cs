﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clickToDismiss : MonoBehaviour {

    public GameObject opButton;
    public int bleedSpeed = 1;

    public int currentVal;
    public int goalVal;

    private IEnumerator coroutine;

    public gameManager gameManagerScript;
    
    public Image sliderObj;

    public bool responsibilitySpawn;
    public int responsibilityIndex;

    public int[] valueChangePerClick = { 0 };
    public string[] resourceOnClick = { "Money" };

    public string title;
    public string flufftext;

    public Text opportunitiesTitleText;
    public Text opportunitiesFluffText;

    // Use this for initialization
    void Start ()
    {
        gameManagerScript = GameObject.Find("_gameManager").GetComponent<gameManager>();
        opportunitiesTitleText = GameObject.Find("OpportunitiesTitle").GetComponent<Text>();
        opportunitiesFluffText = GameObject.Find("OpportunitiesFluffText").GetComponent<Text>();

        opportunitiesTitleText.text = title;
        opportunitiesFluffText.text = flufftext;

        changeSlider();

        coroutine = bleedValue(bleedSpeed);
        StartCoroutine(coroutine);
    }
	
	public void dismissOnClickYes()
    {
        if(responsibilitySpawn)
        {
            gameManagerScript.giveResponsibility(responsibilityIndex);
        }
        
        for (int i = 0; i < resourceOnClick.Length; i++)
        {
            //Debug.Log(gameManagerScript.values[resourceOnClick]);
            gameManagerScript.modifyValue(resourceOnClick[i], valueChangePerClick[i]);
        }
        
        opportunitiesTitleText.text = "Opportunities";
        opportunitiesFluffText.text = "Opportunities come and go, but aint nothing gonna drop out of the sky for your lazy ass, go get up and go seek out some";

        opButton.SetActive(true);
        Destroy(gameObject);
    }
    public void dismissOnClickNo()
    {
        opportunitiesTitleText.text = "Opportunities";
        opportunitiesFluffText.text = "Opportunities come and go, but aint nothing gonna drop out of the sky for your lazy ass, go get up and go seek out some";

        opButton.SetActive(true);
        Destroy(gameObject);
    }

    public void emptyBar()
    {
        opportunitiesTitleText.text = "Opportunities";
        opportunitiesFluffText.text = "Opportunities come and go, but aint nothing gonna drop out of the sky for your lazy ass, go get up and go seek out some";

        opButton.SetActive(true);
        Destroy(gameObject);
    }

    private IEnumerator bleedValue(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            currentVal --;
            changeSlider();

            if (currentVal <= 0)
            {
                emptyBar();
            }
        }
    }
    
    void changeSlider()
    {
        float newVal = ((float)currentVal / (float)goalVal);

        if (newVal == 0)
        {
            newVal = 0.01f;
        }

        sliderObj.fillAmount = newVal;
    }
}
