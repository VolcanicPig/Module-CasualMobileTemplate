using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using TMPro; 

namespace VolcanicPig.Mobile.Ui
{
    public class EndGameMenu : Menu
    {
        [SerializeField] private GameObject winScreen, loseScreen;
        [SerializeField] private TextMeshProUGUI titleText; 

        public override void OnMenuOpened()
        {
            base.OnMenuOpened();

            bool wonGame = GameManager.Instance.GetWinState == WinState.Win;
            
            winScreen.SetActive(wonGame);
            loseScreen.SetActive(!wonGame);

            if(titleText) titleText.text = wonGame ? 
                $"Level {GameManager.Instance.Level} Complete!" : 
                $"Level {GameManager.Instance.Level} Failed";
        }

        public void RestartButtonPressed()
        {
            GlobalFade.Instance.FadeCallback(GameManager.Instance.RestartGame); 
        }
    }
}

