﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public NavMeshAgent agent;
    public CharacterFlipbook flipbook;
    public Vector3 forward;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (agent.velocity.magnitude > 0.01f)
        {
            forward = agent.velocity.normalized;
        }
        flipbook.CalculateFacing(forward);
        if (agent.hasPath)
        {
            flipbook.walking = true;
        }
        else
        {
            flipbook.walking = false;
        }
        Shader.SetGlobalVector("_PlayerPosition", transform.position);
    }

    public void SetDestination(Vector3 target)
    {
        agent.SetDestination(target);
    }

    public bool GetMouseTarget(out Vector3 target)
    {
        int walkableMask = 1 << NavMesh.GetAreaFromName("Walkable");
        Vector3 v = new Vector3(Input.mousePosition.x / (float)Screen.width, Input.mousePosition.y / (float)Screen.height);
        Ray ray = Camera.main.ViewportPointToRay(v);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000.0f))
        {
            // don't walk towards clickable objects
            if (hit.transform.gameObject.tag != "Clickable")
            {
                target = hit.point;
                return true;
            }
        }
        target = Vector3.zero;
        return false;
    }
}
