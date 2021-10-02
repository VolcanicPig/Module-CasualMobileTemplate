using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace VolcanicPig.Mobile.Ui
{
    public class PooledCoinFeedback : PooledObjectBase
    {
        private Vector3 _startPosition;
        private RectTransform _rect;

        private void Start()
        {
            _rect = GetComponent<RectTransform>();
            _startPosition = transform.position;
        }

        public void MoveTo(Vector3 fromPosition, Vector3 toPosition, float duration)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.AppendCallback(() => { transform.position = fromPosition; })
                .Append(transform.DOMove(toPosition, duration))
                .AppendCallback(() => {
                    transform.position = _startPosition;
                    Recycle();
                });

            sequence.Play();
        }

        public void JumpMoveTo(Vector3 fromPosition, Vector3 toPosition, float duration, Ease ease, DOTweenAnimation coinPouchImage)
        {
            if (!_rect)
            {
                _rect = GetComponent<RectTransform>(); 
            }
            
            Sequence sequence = DOTween.Sequence();

            float randScale = Random.Range(1.3f, 1.7f);
            float jump = Random.Range(30, 60);

            sequence.PrependCallback(() =>
            {
                transform.position = fromPosition;
                transform.localScale = Vector3.zero;
            });

            sequence
                .Append(transform.DOJump(GetRandomJumpPoint(fromPosition), jump, 1, .3f))
                .Join(transform.DOScale(new Vector3(randScale, randScale, randScale), .3f))
                .Join(transform.DOLocalRotate(new Vector3(0, 0, 720), .3f, RotateMode.LocalAxisAdd))
                .Append(transform.DOMove(toPosition, duration).SetEase(ease))
                .Join(transform.DOLocalRotate(new Vector3(0, 0, 720), duration, RotateMode.LocalAxisAdd))
                .Join(transform.DOScale(new Vector3(.5f, .5f, .5f), duration))
                .AppendCallback(() =>
                {
                    coinPouchImage.DORewindAndPlayNext();
                    Recycle();
                });
        }

        private Vector3 GetRandomJumpPoint(Vector3 fromPosition)
        {
            float minX = (Screen.width / 10) * -1;
            float maxX = Screen.width / 10;
            float minY = (Screen.height / 10) * -1;
            float maxY = Screen.height / 10;

            Vector3 finalPos = fromPosition;
            float xOffset = Random.Range(minX, maxX);
            float yOffset = Random.Range(minY, maxY);

            finalPos.x += xOffset;
            finalPos.y += yOffset;

            return finalPos;
        }
    }
}
