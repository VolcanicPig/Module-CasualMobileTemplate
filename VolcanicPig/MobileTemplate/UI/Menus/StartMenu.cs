using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VolcanicPig.Mobile.Ui
{
    public class StartMenu : Menu
    {
        public void StartGamePressed()
        {
            MobileGameManager.Instance.StartGame();
        }
    }
}
