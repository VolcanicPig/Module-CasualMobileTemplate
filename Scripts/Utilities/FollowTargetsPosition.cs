using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VolcanicPig.Utilities
{
    public class FollowTargetsPosition : MonoBehaviour
    {
        [SerializeField]
        private Transform target;

        [SerializeField]
        private bool followX, followY, followZ;

        [SerializeField]
        private Vector3 offset;

        private void Update()
        {
            Vector3 currentPos = transform.position;
            Vector3 targetPos = target.position;

            if (followX)
            {
                currentPos.x = targetPos.x + offset.x;
            }

            if (followY)
            {
                currentPos.y = targetPos.y + offset.y;
            }

            if (followZ)
            {
                currentPos.z = targetPos.z + offset.z;
            }

            transform.position = currentPos;
        }
    }
}
