using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Naren
{
    public class UIGameplayManagerBehaviour : MonoBehaviour
    {
        [SerializeField] Text moneyText;
        int currentMoney = 0;

        [SerializeField] Text timeText;
        int currentMonth = 0;
        [SerializeField] int maxMonthPerYear = 4;
        int currentYear = 5;
        [SerializeField] int deathYear = 68;

        private void Start()
        {
            BaseGameManager.OnNextTurn += OnNextTurn;
        }

        private void Update()
        {
            moneyText.text = currentMoney + " Rupee";
            timeText.text = (currentMonth * (12 / maxMonthPerYear)) + " month " + currentYear + " years";
        }

        private void OnNextTurn()
        {

        }
    }
}
