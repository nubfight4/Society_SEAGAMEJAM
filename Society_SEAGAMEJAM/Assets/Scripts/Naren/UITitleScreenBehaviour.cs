using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITitleScreenBehaviour : MonoBehaviour {
    public void ChangeScene(string gameplayScene)
    {
        SoundManagerScript.mInstance.PlaySFX(AudioClipID.SFX_MOUSE_CLICK);
        StartCoroutine(ChangeSceneEnumerator(gameplayScene));
    }

    IEnumerator ChangeSceneEnumerator(string gameplayScene)
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(gameplayScene);
    }
}
