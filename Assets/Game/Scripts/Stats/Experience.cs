using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using System;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour,ISaveable
    {
        [SerializeField] float experincePoints = 0;

        public event Action onExperienceGained;

        public void GainExperience(float exp)
        {
            experincePoints += exp;
            onExperienceGained();
        }

        public float GetExperiences()
        {
            return experincePoints;
        }

        public object CaptureState()
        {
            return experincePoints;
        }

        public void RestoreState(object state)
        {
            experincePoints = (float)state;
        }
        
    }    
}
