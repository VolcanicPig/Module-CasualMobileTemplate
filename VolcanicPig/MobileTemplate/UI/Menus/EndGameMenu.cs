using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VolcanicPig.Mobile.Ui
{
    public class EndGameMenu : Menu
    {
        [SerializeField] private GameObject winScreen, loseScreen;

        public override void OnMenuOpened()
        {
            base.OnMenuOpened();

            bool wonGame = MobileGameManager.Instance.GetWinState == WinState.Win;
            winScreen.SetActive(wonGame);
            loseScreen.SetActive(!wonGame);
        }

        public void RestartButtonPressed()
        {
            MobileGameManager.Instance.RestartGame();
        }
    }
}

