using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINotificationBehaviour : MonoBehaviour
{
    public static UINotificationBehaviour instance;

    public static UINotificationBehaviour Instance { get { return instance; } }

    [SerializeField] Text titleText;
    [SerializeField] Text descText;
    [SerializeField] Animator imageAnimator;
    [SerializeField] GameObject OKEventUI;

    private void Start()
    {
        SetupSingleton();
    }

    private void SetupSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void ShowNotification(string title, string desc, int imageId)
    {
        OKEventUI.SetActive(true);
        titleText.text = title;
        descText.text = desc;
        //imageAnimator.SetInteger("imageId", imageId);
    }

    public void HideNotification()
    {
        OKEventUI.SetActive(false);
    }
}
