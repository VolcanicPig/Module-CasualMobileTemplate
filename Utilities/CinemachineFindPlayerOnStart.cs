using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Game; 

namespace VolcanicPig.Mobile
{
    public class CinemachineFindPlayerOnStart : MonoBehaviour
    {
        private CinemachineVirtualCamera _cam;

        private void OnEnable()
        {
            MobileGameManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnDisable()
        {
            MobileGameManager.OnGameStateChanged -= OnGameStateChanged;
        }

        private void Start()
        {
            _cam = GetComponent<CinemachineVirtualCamera>();
        }

        private void OnGameStateChanged(GameState state)
        {
            if (state == GameState.Start)
            {
                SetPlayer();
            }
        }

        public void SetPlayer()
        {
            GameObject player = MobileGameManager.Instance.GetCurrentPlayer;

            _cam.m_Follow = player.transform;
            _cam.m_LookAt = player.transform;
        }

    }
}
