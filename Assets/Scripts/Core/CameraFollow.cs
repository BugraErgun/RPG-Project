using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] Transform PlayerTransform;
       

        void LateUpdate()
        {
            transform.position = PlayerTransform.position;
        }
    }
}
