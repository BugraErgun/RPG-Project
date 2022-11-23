using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Controller {

    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance;
        [SerializeField] float suspictionTime = 5f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float wayPointTolerance = 1f;
        [SerializeField] float waypointLifeTime = 3f;

        [SerializeField] float patrolSpeedFraction = 0.5f;

        GameObject player;

        Fighter fighter;
        Health health;
        Mover mover;

        Vector3 startPos;
        float timeSinceLastSawPlayer;
        float timeSinceArrivedWaypoint;
        int currentWayPointsIndex = 0;


        void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();

            startPos = transform.position;
        }

     
        void Update()
        {
            if (health.IsDead())
            {
                return;
            }


           if( PlayerToDistance() < chaseDistance &&  fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                fighter.Attack(player);
            }
            else if (timeSinceLastSawPlayer<suspictionTime)
            {
                GetComponent<ActionScheduler>().CangelCurrentAction();
            }
            else
            {
                Vector3 nextPos = startPos;
                if (patrolPath !=null)
                {
                    if (AtWayPoint())
                    {
                        timeSinceArrivedWaypoint = 0;
                        CycleWayPoint();
                    }

                    nextPos = GetNextWaypoint();

                }
                if (timeSinceArrivedWaypoint > waypointLifeTime) 
                {
                    mover.StartMoveAction(nextPos,patrolSpeedFraction);
                }
               
            }

            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedWaypoint += Time.deltaTime;

        }

        private bool AtWayPoint()
        {
            float distanceWayPoint = Vector3.Distance(transform.position, GetNextWaypoint());
            return distanceWayPoint < wayPointTolerance;
        }

        private void CycleWayPoint()
        {
            currentWayPointsIndex = patrolPath.GetNextIndex(currentWayPointsIndex);
        }
        private Vector3 GetNextWaypoint()
        {
           return patrolPath.GetWayPointPisiton(currentWayPointsIndex);
        }


        private float PlayerToDistance()
        {
            return Vector3.Distance(player.transform.position, transform.position);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
