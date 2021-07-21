using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace VolcanicPig.Mobile.Ui
{
    public class GlobalFade : SingletonBehaviour<GlobalFade>
    {
        private CanvasGroup _canvas;

        private void Start()
        {
            _canvas = GetComponent<CanvasGroup>();

            if (_canvas.alpha != 0)
            {
                FadeOff();
            }
        }

        public void FadeOn(float duration = 1f)
        {
            _canvas.DOFade(1f, duration);
        }

        public void FadeOff(float duration = 1f)
        {
            _canvas.DOFade(0f, duration);
        }

        public void FadeCallback(Action callback, float duration = 1f, float midActionPause = .5f)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.AppendCallback(() => { FadeOn(duration); })
            .AppendInterval(duration)
            .AppendCallback(() => { callback.Invoke(); })
            .AppendInterval(midActionPause)
            .AppendCallback(() => { FadeOff(duration); });

            sequence.Play();
        }
    }
}
