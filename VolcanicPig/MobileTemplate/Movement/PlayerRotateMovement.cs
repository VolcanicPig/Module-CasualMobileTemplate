using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolcanicPig.Mobile.Gestures;

namespace VolcanicPig.Mobile.Movement
{
    public class PlayerRotateMovement : PlayerMovement
    {
        [SerializeField] private GestureController gestures;
        [SerializeField] private float rotateSpeed; 

        private void Update()
        {
            HandleRotation();
        }

        private void HandleRotation()
        {
            Vector2 touchDelta = gestures.TouchDelta;
            float step = rotateSpeed * Time.deltaTime; 

            transform.Rotate(Vector3.up * touchDelta.x * step); 
        }
    }
}
