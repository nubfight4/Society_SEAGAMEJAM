using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SoundManagerScript.mInstance.PlayBGM(AudioClipID.BGM);
	}

}
