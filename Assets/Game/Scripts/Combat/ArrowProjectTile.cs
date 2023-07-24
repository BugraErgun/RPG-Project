using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using RPG.Attributes;

public class ArrowProjectTile : MonoBehaviour
{
    Health target;
    [SerializeField] private float speed;
    [SerializeField] private GameObject hitEffect = null;
    [SerializeField] private float maxLifeTime = 10f;

    GameObject instigator = null;


    float damage = 0;

    void Update()
    {
        if (target==null)
        {
            return;
        }
      
        transform.LookAt(GetAimLocation());
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void SetTarget(Health health,GameObject instigator,float damage)
    {
        this.target = health;
        this.damage = damage;
        this.instigator = instigator;

        Destroy(gameObject,maxLifeTime);
    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetColl = target.GetComponent<CapsuleCollider>();

        if (targetColl==null)
        {
            return target.transform.position;
        }
        return target.transform.position + Vector3.up * targetColl.height / 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() != target)
        {
            return;
        }
        target.TakeDamage(instigator,damage);

        if (hitEffect != null)
        {
            Instantiate(hitEffect,GetAimLocation(),transform.rotation);
        }

     

        Destroy(gameObject);
    }
}
