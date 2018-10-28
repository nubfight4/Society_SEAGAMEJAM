using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openPanelScript : MonoBehaviour
{
    public RectTransform panel;

    public openPanelScript panelScript;

    public Vector3 panelPos;

    private Vector3 oldpanelPos;

    public bool isOpen;

    public gameManager gameManagerScript;

    public string name;

    void Start()
    {
        gameManagerScript = GameObject.Find("_gameManager").GetComponent<gameManager>();
        
        oldpanelPos = panel.localPosition;
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
    }

    public void closePanelFucnt()
    {
        panel.localPosition = oldpanelPos;
    }
}
