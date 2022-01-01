using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VolcanicPig.Mobile.Gestures
{
    public class GestureController : SingletonBehaviour<GestureController> 
    {
        public static Action<Vector2> OnTouchDown;
        public static Action<Vector2> OnTouchUp;
        public static Action<Vector2> OnTap;
        public static Action<Vector2> OnSwipe;

        private Vector2 _touchDelta;
        public Vector2 TouchDelta => _touchDelta;

        private Vector2 _scaledTouchDelta;
        public Vector2 ScaledTouchDelta => _scaledTouchDelta;

        private Vector2 _axis; 
        public Vector2 Axis => _axis;

        private bool _isTouching;
        public bool Touching => _isTouching;

        [Header("Swipe Settings")]
        [SerializeField] private float swipeThreshold = 1f;

        [Header("Tap Settings")]
        [SerializeField] private float maxTapTime = 1f;
        [SerializeField] private Vector2 minTapCancelDist = new Vector2(50, 50); 

        private Vector2 _lastMousePos;
        private Vector2 _touchDownPos;
        private Vector2 _touchUpPos;
        private float _touchDownTime;
        private bool _cancelTap; 


        private void Update()
        {
#if UNITY_EDITOR
            CheckGesturesEditor();
#else
            CheckGesturesMobile();
#endif
        }

        private void CheckGesturesMobile()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    _touchDownPos = touch.position; 
                    _touchDownTime = Time.time;
                    _isTouching = true;

                    OnTouchDown?.Invoke(touch.position);
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    _touchUpPos = touch.position; 
                    Vector2 dragDistanceUnscaled = touch.deltaPosition;

                    _scaledTouchDelta = new Vector2(dragDistanceUnscaled.x / Screen.width,
                        dragDistanceUnscaled.y / Screen.height);

                    float _horizontalAxis = touch.position.x - _touchDownPos.x; 
                    float _verticalAxis = touch.position.y -_touchDownPos.y;
                    _axis = new Vector2(_horizontalAxis, _verticalAxis).normalized;

                    if (CheckSwipe())
                    {
                        _touchDownPos = touch.position; 
                    }
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    _touchUpPos = touch.position;
                    _isTouching = false;

                    if (DidTap(touch.position))
                    {
                        OnTap?.Invoke(touch.position); 
                    }

                    OnTouchUp.Invoke(touch.position);
                }
            }
            else
            {
                _touchDelta = Vector2.zero;
                _scaledTouchDelta = Vector2.zero; 
                _axis = Vector2.zero; 
            }
        }

        private void CheckGesturesEditor()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = Input.mousePosition;
                _lastMousePos = mousePos;  
                _touchDownPos = mousePos;
                _touchDownTime = Time.time;
                _isTouching = true;

                OnTouchDown?.Invoke(mousePos);
            }

            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = Input.mousePosition;
                _touchUpPos = mousePos; 
                _touchDelta = mousePos - _lastMousePos;
                _scaledTouchDelta = new Vector2(_touchDelta.x / Screen.width,
                    _touchDelta.y / Screen.height);

                _lastMousePos = mousePos; 

                float _horizontalAxis = mousePos.x - _touchDownPos.x; 
                float _verticalAxis = mousePos.y -_touchDownPos.y;

                _axis = new Vector2(_horizontalAxis, _verticalAxis).normalized;

                if (CheckSwipe())
                {
                    _touchDownPos = mousePos;
                }
            }
            else
            {
                _touchDelta = Vector2.zero;
                _scaledTouchDelta = Vector2.zero;
                _axis = Vector2.zero; 
            }

            if (Input.GetMouseButtonUp(0))
            {
                Vector2 mousePos = Input.mousePosition; 
                _touchUpPos = mousePos;
                _isTouching = false;

                if (DidTap(mousePos))
                {
                    OnTap?.Invoke(mousePos);
                }

                OnTouchUp?.Invoke(Input.mousePosition);
            }
        }

        private bool DidTap(Vector2 touchPos)
        {
            return Time.time < (_touchDownTime + maxTapTime) &&
                (touchPos.x - _touchDownPos.x) < minTapCancelDist.x &&
                (touchPos.y - _touchDownPos.y) < minTapCancelDist.y;
        }

        private bool CheckSwipe()
        {
            if (VerticalMove() > swipeThreshold && VerticalMove() > HorizontalMove())
            {
                if (_touchDownPos.y - _touchUpPos.y > 0)
                {
                    OnSwipe?.Invoke(new Vector2(0, -1));
                    return true;
                }
                else
                {
                    OnSwipe?.Invoke(new Vector2(0, 1));
                    return true;
                }
            }
            else if (HorizontalMove() > swipeThreshold && HorizontalMove() > VerticalMove())
            {
                if (_touchDownPos.x - _touchUpPos.x > 0)
                {
                    OnSwipe?.Invoke(new Vector2(-1, 0));
                    return true;
                }
                else
                {
                    OnSwipe?.Invoke(new Vector2(1, 0));
                    return true;
                }
            }

            return false;
        }

        private float VerticalMove()
        {
            return Mathf.Abs(_touchUpPos.y - _touchDownPos.y);
        }

        private float HorizontalMove()
        {
            return Mathf.Abs(_touchUpPos.x - _touchDownPos.x);
        }
    }
}
