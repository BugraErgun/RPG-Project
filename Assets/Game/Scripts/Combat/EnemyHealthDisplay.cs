using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using TMPro;
using UnityEngine;


namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;

        private void Awake()
        {
            fighter = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            Health health = fighter.GetTarget();

            if (fighter.GetTarget()==null )
            {
                GetComponent<TextMeshProUGUI>().text = "Enemy Health:N/A";
                return;
            }

            GetComponent<TextMeshProUGUI>().text = String.Format("Enemy Health: {0:0}/{1:0}",
                health.GetHealthPoints(), health.GetMaxHealthPoints());
        }
    }
}
