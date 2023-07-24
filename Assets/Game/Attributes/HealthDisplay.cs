using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;

        private void Awake()
        {
            health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        }

        private void Update()
        {
            GetComponent<TextMeshProUGUI>().text = String.Format("Player Health: {0:0}/{1:0}",
                health.GetHealthPoints(),health.GetMaxHealthPoints());
        }
    }
}
