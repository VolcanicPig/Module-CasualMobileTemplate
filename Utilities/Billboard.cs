using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField] private bool faceAway;

        private void LateUpdate()
        {
            if (!faceAway)
            {
                transform.LookAt(Helpers.Camera.transform);
            }
            else
            {
                var position = transform.position;
                Vector3 dir = position - Helpers.Camera.transform.position;
                transform.LookAt(position + (dir * 4));
            }
        }
    }
}
