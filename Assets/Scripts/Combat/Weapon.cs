using UnityEngine;
using RPG.Core;


namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make new weapon.", order = 0)]

    public class Weapon:ScriptableObject
    {
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] AnimatorOverrideController animationOverride = null;

        [SerializeField] float weaponRange=2;
        [SerializeField] float weaponDamage = 10;
        [SerializeField] bool isRightHand = true;
        [SerializeField] Projecttile projecttile = null;

        public void Spawn(Transform rightHand,Transform leftHand,Animator animator)
        {
            if (weaponPrefab != null)
            {
                Transform handTransform;


                if (isRightHand)
                {
                    handTransform = rightHand;
                }
                else
                {
                    handTransform = leftHand;
                }

                Instantiate(weaponPrefab, handTransform);

            }
            if (animationOverride != null)
            {
                animator.runtimeAnimatorController = animationOverride;

            }
        }

        public float GetDamage()
        {
            return weaponDamage;
        }
        public float GetRange()
        {
            return weaponRange;
        }

        public bool HasProjectTile()
        {
            return projecttile != null;
        }
        public void LaunchProjectTile(Transform rightHand,Transform leftHand,Health target)
        {
            Transform handTransform;


            if (isRightHand)
            {
                handTransform = rightHand;
            }
            else
            {
                handTransform = leftHand;
            }
            Projecttile projecttileInstantiate = Instantiate(projecttile, handTransform.position,Quaternion.identity);
            projecttile.SetTarget(target);
        }

    }

}
