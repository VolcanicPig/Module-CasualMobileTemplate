using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace VolcanicPig.Mobile.Ui
{
    public class FeedbackEffects : SingletonBehaviour<FeedbackEffects>
    {
        public int maxCoinsToShow = 20;

        [SerializeField] private Transform feedbackParent; 
        [SerializeField] private DOTweenAnimation coinPouchImage;

        [Header("Reference Positions")]
        [SerializeField] private Transform coinPotPosition;
        [SerializeField] private Transform middlePosition;

        private Camera _cam;

        private void Start()
        {
            _cam = Camera.main; 
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                DoCoinFeedback(200, middlePosition.position);
            }
        }

        public void DoCoinFeedback(int coinsEarned, Vector3 fromPosition, float duration = 0.5f, float betweenWait = 0.1f, bool fromWorldPosition = false)
        {
            if (coinsEarned > maxCoinsToShow)
            {
                coinsEarned = maxCoinsToShow;
            }
            
            if (fromWorldPosition)
            {
                fromPosition = _cam.WorldToScreenPoint(fromPosition); 
            }

            StartCoroutine(CoCoinFeedback(coinsEarned, fromPosition, duration, betweenWait));
        }

        private IEnumerator CoCoinFeedback(int coinsToShow, Vector3 fromPosition, float duration, float betweenWait)
        {
            for (int i = 0; i < coinsToShow; i++)
            {
                PooledCoinFeedback coin = ObjectPool.Instance.SpawnFromPool("CoinFeedback", feedbackParent, fromPosition, Quaternion.identity) as PooledCoinFeedback;
                
                if(coin) 
                    coin.JumpMoveTo(fromPosition, coinPotPosition.position, duration, Ease.Linear, coinPouchImage);
                
                yield return new WaitForSeconds(betweenWait);
            }
        }
    }
}
