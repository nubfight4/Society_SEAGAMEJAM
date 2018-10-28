﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openPanelScript : MonoBehaviour
{
    public RectTransform panel;
    public RectTransform scrollbar;

    public openPanelScript panelScript;

    public Vector3 panelPos;
    public Vector3 scrollbarPos;

    private Vector3 oldpanelPos;
    private Vector3 oldscrollbarPos;

    public bool isOpen;

    public gameManager gameManagerScript;

    public string name;

    void Start()
    {
        gameManagerScript = GameObject.Find("_gameManager").GetComponent<gameManager>();
        
        oldpanelPos = panel.localPosition;
        oldscrollbarPos = scrollbar.localPosition;
    }

    public void onClickChange()
    {
        SoundManagerScript.mInstance.PlaySFX(AudioClipID.SFX_MOUSE_CLICK);
        if(isOpen)
        {
            gameManagerScript.changeText(name, "OPEN");

            isOpen = false;
            closePanelFucnt();

            gameManagerScript.removeCriticalRespo();
        }
        else if (!isOpen)
        {
            if (panelScript.isOpen)
            {
                panelScript.onClickChangeSpecial();
            }

            isOpen = true;
            openPanelFucnt();
            gameManagerScript.changeText(name, "CLOSE");

            gameManagerScript.removeCriticalRespo();
        }
    }

    public void onClickChangeSpecial()
    {
        if (isOpen)
        {
            gameManagerScript.changeText(name, "OPEN");

            isOpen = false;
            closePanelFucnt();

            gameManagerScript.removeCriticalRespo();
        }
        else if (!isOpen)
        {
            isOpen = true;
            openPanelFucnt();
            gameManagerScript.changeText(name, "CLOSE");

            gameManagerScript.removeCriticalRespo();
        }
    }

    public void openPanelFucnt()
    {
        panel.localPosition = panelPos;
        scrollbar.localPosition = scrollbarPos;
    }

    public void closePanelFucnt()
    {
        panel.localPosition = oldpanelPos;
        scrollbar.localPosition = oldscrollbarPos;
    }
}
