using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace VolcanicPig.Mobile.Ui
{
    public class FeedbackEffects : SingletonBehaviour<FeedbackEffects>
    {
        public int maxCoinsToShow = 20;

        [SerializeField] private DOTweenAnimation coinPouchImage;

        [Header("Reference Positions")]
        [SerializeField] private Transform coinPotPosition;
        [SerializeField] private Transform middlePosition;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                DoCoinFeedback(200, middlePosition, .5f, .1f);
            }
        }

        public void DoCoinFeedback(int coinsEarned, Transform fromPosition, float duration, float betweenWait)
        {
            int coinsToShow = coinsEarned / 20;

            if (coinsToShow > maxCoinsToShow)
            {
                coinsToShow = maxCoinsToShow;
            }

            StartCoroutine(CoCoinFeedback(coinsToShow, fromPosition, duration, betweenWait));
        }

        private IEnumerator CoCoinFeedback(int coinsToShow, Transform fromPosition, float duration, float betweenWait)
        {
            for (int i = 0; i < coinsToShow; i++)
            {
                PooledCoinFeedback coin = ObjectPool.Instance.SpawnFromPool("CoinFeedback", transform, fromPosition.position) as PooledCoinFeedback;
                coin.JumpMoveTo(fromPosition.position, coinPotPosition.position, duration, Ease.Linear, coinPouchImage);
                yield return new WaitForSeconds(betweenWait);
            }
        }
    }
}
