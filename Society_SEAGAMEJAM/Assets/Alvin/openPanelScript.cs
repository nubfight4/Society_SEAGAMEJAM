using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openPanelScript : MonoBehaviour
{
    public RectTransform panel;
    public RectTransform scrollbar;

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
        if(isOpen)
        {
            gameManagerScript.changeText(name, "Open");

            isOpen = false;
            closePanelFucnt();
        }
        else if (!isOpen)
        {
            isOpen = true;
            openPanelFucnt();
            gameManagerScript.changeText(name, "Close");
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
