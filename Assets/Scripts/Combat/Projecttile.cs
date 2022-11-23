using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{

    public class Projecttile : MonoBehaviour
    {
         
        [SerializeField] float arrowSpeed = 1f;

        Health Target;


        void Update()
        {
            if (Target==null)
            {
                return;

            }
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward*arrowSpeed*Time.deltaTime);


        }

        public void SetTarget(Health target)
        {
            this.Target = target;
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider capsuleCollider = Target.GetComponent<CapsuleCollider>();
            if (Target==null)
            {
                return Target.transform.position;
            }
            return Target.transform.position + Vector3.up * capsuleCollider.height / 2;

        }
    }
}
