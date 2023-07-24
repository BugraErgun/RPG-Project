using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Movement;
using RPG.Core;
using RPG.Attributes;
using System;
using UnityEngine.EventSystems;
using RPG.Control;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{    
    private Health health;

    [System.Serializable]
    struct CursorMapping
    {
        public CursorType type;
        public Texture2D texture;
        public Vector2 hotspot;
    }

    [SerializeField] CursorMapping[] cursorMappings = null;

    [SerializeField] float maxNavMeshProjectionDistance = 1f;
    [SerializeField] float maxNavPathLength = 40f;

    private void Awake()
    {
        health = GetComponent<Health>();
    }
   
    void Update()
    {

        if (health.IsDead())
        {
            SetCursor(CursorType.None);
            return;
        }

        if (InteractWithComponent()) return;
        if (InteractWithMovement()) return;

        SetCursor(CursorType.None);
    }

    private bool InteractWithComponent()
    {
        RaycastHit[] hits = RaycastAllSorted();
        foreach (RaycastHit hit in hits)
        {
            IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
            foreach (IRaycastable raycastable in raycastables)
            {
                if (raycastable.HandleRaycast(this))
                {
                    SetCursor(raycastable.GetCursorType());
                    return true;
                }
            }
        }
        return false;
    }

   

    private RaycastHit[] RaycastAllSorted()
    {
        RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

        float[] distances = new float[hits.Length];

        for (int i = 0; i < hits.Length; i++)
        {
            distances[i] = hits[i].distance;
        }

        Array.Sort(distances, hits);

        return hits;
    }

    private bool InteractWithMovement()
    {
        Vector3 target;
        bool hasHit = RaycastNavMesh(out target);

        if (hasHit)
        {
            if (Input.GetMouseButton(0))
            {
                GetComponent<Mover>().StartMoveAction(target, 1f);
            }

            SetCursor(CursorType.Movement);
            return true;
        }
        return false;
    }

    private bool RaycastNavMesh(out Vector3 target)
    {
        target = new Vector3();

        RaycastHit hit;
        bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

        if (!hasHit)
        {
            return false;
        }

        NavMeshHit navMeshHit;
        bool hastCastToNavMesh =  NavMesh.SamplePosition(
            hit.point, out navMeshHit, maxNavMeshProjectionDistance, NavMesh.AllAreas);

        if (!hastCastToNavMesh)
        {
            return false;
        }

        target = navMeshHit.position;

        NavMeshPath path = new NavMeshPath();
        bool hasPath =  NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas,path);

        if (!hasPath) return false;
        
        if (path.status != NavMeshPathStatus.PathComplete) return false;

        if (GetPathLenght(path) > maxNavPathLength) return false;

        return true;
    }

    private float GetPathLenght(NavMeshPath path)
    {
        float total = 0;

        if (path.corners.Length<2f)
        {
            return total;
        }

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            float distance = Vector3.Distance(path.corners[i], path.corners[i + 1]);
        }

        return total;
    }

    private void SetCursor(CursorType cursorType)
    {
        CursorMapping mapping = GetCursorMapping(cursorType);
        Cursor.SetCursor(mapping.texture,mapping.hotspot,CursorMode.Auto);
    }

    private CursorMapping GetCursorMapping(CursorType type)
    {
        foreach (CursorMapping mapping in cursorMappings)
        {
            if (mapping.type == type)
            {
                return mapping;
            }
        }
        return cursorMappings[0];
    }

    private static Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
