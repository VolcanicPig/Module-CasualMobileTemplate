using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace VolcanicPig.Mobile.Ui
{
    public class StartMenu : Menu
    {
        public void StartGamePressed()
        {
            GameManager.Instance.StartGame();
        }
    }
}
