using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolcanicPig.Mobile.Gestures;

namespace VolcanicPig.Mobile.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerSlideMovement : PlayerMovement
    {
        [Header("Components")]
        [SerializeField] private GestureController gestures;
        [SerializeField] private float sideMult, sideSpeed;

        private void Update()
        {
            SidewaysMovement();
        }

        private void SidewaysMovement()
        {
            if (!CanMove) return;
            Vector3 cachedPosition = transform.position;
            Vector2 touchDelta = gestures.TouchDelta;

            cachedPosition.x += touchDelta.x * sideMult * Time.deltaTime;
            cachedPosition.x = Mathf.Clamp(cachedPosition.x, -5, 5);

            float step = sideSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, cachedPosition, step);
        }
    }
}
