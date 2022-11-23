using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour,IAction
    {
        NavMeshAgent agent;
        Animator anim;
        Health health;

        [SerializeField] float maxSpeed = 5f;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            health = GetComponent<Health>();
        }


        void Update()
        {
            agent.enabled = !health.IsDead();

            UpdateAnimator();

        }


        public void StartMoveAction(Vector3 hit,float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            GetComponent<Fighter>().Cancel();
            MoveTo(hit, speedFraction);
        }


        public void MoveTo(Vector3 hit,float speedFraction)
        {
            agent.speed = maxSpeed * speedFraction;
            agent.destination = hit;
            agent.isStopped = false;
        }

        public void Cancel()
        {
            agent.isStopped = true;
        }

        

        private void UpdateAnimator()
        {
            Vector3 velocity = agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            float speed = localVelocity.z;
            anim.SetFloat("moveSpeed", speed);
        }
    }
}

