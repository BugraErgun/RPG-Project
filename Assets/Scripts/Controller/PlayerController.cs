using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Controller
{

    public class PlayerController : MonoBehaviour
    {
        Health health;

        private void Start()
        {
            health = GetComponent<Health>();
        }

        void Update()
        {
            if (health.IsDead()==true)
            {
                return;
            }


            if (InteractWithCombat() == true)
            {

                return;
            }
            if (InteractWithMovement()==true)
            {
                
                return;
            }
           
            
        }

     

        private bool InteractWithCombat()
        {
            
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                if (target==null)
                {
                    continue;
                }


                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }

                if (target== null)
                {
                    continue;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                    
                }

                return true;

            }

            return false;

        }

        

        private bool InteractWithMovement()
        {
           
            RaycastHit hit;
            bool hitting = Physics.Raycast(GetMouseRay(), out hit);

            if (hitting)
            {
                if (Input.GetMouseButton(1))
                {

                    GetComponent<Mover>().StartMoveAction(hit.point,1f) ;
                    
                }
                return true;
            }
            return false;
        }
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
