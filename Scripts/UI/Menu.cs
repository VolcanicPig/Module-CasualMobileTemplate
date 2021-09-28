using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace VolcanicPig.Mobile.Ui
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class Menu : MonoBehaviour
    {
        [SerializeField] private string menuId;
        public string MenuId => menuId;

        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();

            if (_canvasGroup)
            {
                _canvasGroup.alpha = 0f;
                _canvasGroup.interactable = false;
                _canvasGroup.blocksRaycasts = false;
            }
        }

        public void Open()
        {
            if (!_canvasGroup) return;

            OnMenuOpened();
            _canvasGroup.DOFade(1, .3f).OnComplete(() =>
            {
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;
            });
        }

        public void Close()
        {
            if (!_canvasGroup) return;

            OnMenuClosed();
            _canvasGroup.DOFade(0, .3f).OnComplete(() =>
            {
                _canvasGroup.interactable = false;
                _canvasGroup.blocksRaycasts = false;
            });
        }

        public virtual void OnMenuOpened() { }

        public virtual void OnMenuClosed() { }

    }
}
