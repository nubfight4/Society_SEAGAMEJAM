using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {

    GameObject PausePanel;
    public bool isPaused;

    private void Start()
    {
        PausePanel = GameObject.FindGameObjectWithTag("PausePanel");
        isPaused = false;
    }

    void Update()
    {
        if(isPaused)
        {
            PausePanel.SetActive(true);
        }
        else
        {
            PausePanel.SetActive(false);
        }

        if (Input.GetButtonDown("Pause"))
        {
            Debug.Log("sad");
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {     
        Time.timeScale = 0.0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;       
        isPaused = false;
    }
}
