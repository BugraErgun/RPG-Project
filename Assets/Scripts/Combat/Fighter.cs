using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{

    public class Fighter : MonoBehaviour,IAction
    {
        [SerializeField] float timeAttack = 1f;
        float timeSinceLastAttack;

       
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHeandTransform = null;

        [SerializeField] Weapon defaultWeapon = null;

       

        Health targetObject;

        private void Start()
        {
            SpawnWeapon(defaultWeapon);
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (targetObject == null)
            {
                return;

            }
            if (targetObject.IsDead()==true)
            {
                GetComponent<Animator>().ResetTrigger("attack");
                Cancel();
                return;
            }
            

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(targetObject.transform.position,1f);
            }
            else
            {
                AttackMethod();
                GetComponent<Mover>().Cancel();
            }
        }

        private void AttackMethod()
        {
            transform.LookAt(targetObject.transform);

            if (timeSinceLastAttack>timeAttack)
            {
                TriggerAttack();

                timeSinceLastAttack = 0;

            }


        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget==null)
            {
                return false;
            }

            Health healthToTest = GetComponent<Health>();
            return healthToTest != null && !healthToTest.IsDead();
        }
        public void Attack(GameObject target)
        {

            GetComponent<ActionScheduler>().StartAction(this);

            targetObject = target.GetComponent<Health>();


        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, targetObject.transform.position) < defaultWeapon.GetRange();
        }

        

        public void Cancel()
        {
            targetObject = null;

            StopAttack();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        public void Hit()
        {
            if (targetObject==null)
            {
                return;
            }
            if (defaultWeapon.HasProjectTile())
            {
                defaultWeapon.LaunchProjectTile(rightHandTransform,leftHeandTransform,targetObject);
            }
            else
            {
                targetObject.TakeDamage(defaultWeapon.GetDamage());
            }
           

        }

        public void SpawnWeapon(Weapon weapon)
        {
            defaultWeapon = weapon;

            Animator anim = GetComponent<Animator>();
            defaultWeapon.Spawn(rightHandTransform,leftHeandTransform, anim);
        }
    }
}

