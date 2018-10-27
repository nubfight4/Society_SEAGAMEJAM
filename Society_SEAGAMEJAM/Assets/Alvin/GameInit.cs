using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour {

	void Start ()
    {
        List<Values> values = new List<Values>();
        values.Add(new Values("Money", 50));
        values.Add(new Values("Health", 100));
    }
}
