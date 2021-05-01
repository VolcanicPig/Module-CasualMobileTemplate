using UnityEngine;
using VolcanicPig.Mobile;

public class MobileGameManager : MobileGameManagerTemplate<MobileGameManager> 
{
    private void OnEnable()
    {
        Player.OnPlayerDeath += OnPlayerKilled;
    }

    private void OnDisable()
    {
        Player.OnPlayerDeath -= OnPlayerKilled;
    }

    private void OnPlayerKilled()
    {
        EndGame(false);
    }
}