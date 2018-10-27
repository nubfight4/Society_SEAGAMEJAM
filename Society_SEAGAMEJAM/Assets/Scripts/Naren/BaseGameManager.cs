using UnityEngine;
using System.Collections;

namespace Naren
{
    public class BaseGameManager : MonoBehaviour
    {
        public delegate void NextTurn();
        public static event NextTurn OnNextTurn;
        public static void DelegateNextTurn()
        {
            if (OnNextTurn != null)
            {
                DelegateNextTurn();
            }
        }

    }
}
