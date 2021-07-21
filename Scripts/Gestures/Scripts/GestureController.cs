using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VolcanicPig.Mobile.Gestures
{
    public class GestureController : SingletonBehaviour<GestureController> 
    {
        public delegate void TouchDown(Vector2 pos);
        public static event TouchDown OnTouchDown;

        public delegate void TouchUp(Vector2 pos);
        public static event TouchUp OnTouchUp;

        private Vector2 _touchDelta;
        public Vector2 TouchDelta => _touchDelta;

        private Vector2 _scaledTouchDelta;
        public Vector2 ScaledTouchDelta => _scaledTouchDelta;

        private Vector2 _axis; 
        public Vector2 Axis => _axis; 

        private Vector2 _lastMousePos;
        private Vector2 _touchDownPos; 


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
                    OnTouchDown?.Invoke(touch.position);
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    Vector2 dragDistanceUnscaled = touch.deltaPosition;

                    _scaledTouchDelta = new Vector2(dragDistanceUnscaled.x / Screen.width,
                        dragDistanceUnscaled.y / Screen.height);

                    float _horizontalAxis = touch.position.x - _touchDownPos.x; 
                    float _verticalAxis = touch.position.y -_touchDownPos.y;
                    _axis = new Vector2(_horizontalAxis, _verticalAxis).normalized; 
                }

                if (touch.phase == TouchPhase.Ended)
                {
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
                OnTouchDown?.Invoke(mousePos);
            }

            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = Input.mousePosition;

                _touchDelta = mousePos - _lastMousePos;
                _scaledTouchDelta = new Vector2(_touchDelta.x / Screen.width,
                    _touchDelta.y / Screen.height);

                _lastMousePos = mousePos; 

                float _horizontalAxis = mousePos.x - _touchDownPos.x; 
                float _verticalAxis = mousePos.y -_touchDownPos.y;

                _axis = new Vector2(_horizontalAxis, _verticalAxis).normalized;  
            }
            else
            {
                _touchDelta = Vector2.zero;
                _scaledTouchDelta = Vector2.zero;
                _axis = Vector2.zero; 
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnTouchUp?.Invoke(Input.mousePosition);
            }
        }
    }
}
