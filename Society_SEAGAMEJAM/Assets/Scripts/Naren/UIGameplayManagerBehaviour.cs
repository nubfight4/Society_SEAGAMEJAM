using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Naren
{
    public class UIGameplayManagerBehaviour : MonoBehaviour
    {
        ExampleData exampleData;

        [SerializeField] Text moneyText;
        [SerializeField] Text timeText;

        [SerializeField] GameObject dreamPanel;

        private void Awake()
        {
            exampleData = GetComponent<ExampleData>();
        }

        private void Update()
        {
            moneyText.text = exampleData.currentMoney + " Rupee";
            timeText.text = exampleData.currentAge + " / " + exampleData.maxAge + " years";
        }
    }
}
