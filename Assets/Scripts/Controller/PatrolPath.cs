using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Controller
{
    public class PatrolPath : MonoBehaviour
    {
        float wayPointRadius = 0.4f;

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);

                Gizmos.DrawSphere(GetWayPointPisiton(i), wayPointRadius);
                Gizmos.DrawLine(GetWayPointPisiton(i), GetWayPointPisiton(j));
            }


        }

        public  int GetNextIndex(int i)
        {
            if (i+1 == transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }

        public Vector3 GetWayPointPisiton(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}
