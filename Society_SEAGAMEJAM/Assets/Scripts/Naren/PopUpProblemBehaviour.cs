using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpProblemBehaviour : MonoBehaviour {
    [SerializeField] openPanelScript openProbPanel;

    [SerializeField] string problemButtonObjectNameToFind = "Problems Button";

    private void Start()
    {
        openPanelScript probPanel = transform.parent.Find(problemButtonObjectNameToFind).GetComponent<openPanelScript>(); // using find child method, please don't change any object name
        openProbPanel.panel = probPanel.panel;
        openProbPanel.panelPos = probPanel.panelPos;
        openProbPanel.panelScript = probPanel.panelScript;
        openProbPanel.gameManagerScript = probPanel.gameManagerScript;
    }

    public void DestroyPopUp()
    {
        Destroy(this.gameObject);
    }
}
