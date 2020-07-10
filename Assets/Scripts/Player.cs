using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    protected NavMeshAgent agent;
    public CharacterFlipbook flipbook;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 target;
            if (GetMouseTarget(out target))
            {
                agent.SetDestination(target);
            }
        }

        if (agent.hasPath)
        {
            flipbook.CalculateFacing(agent.velocity);
            flipbook.walking = true;
        } else
        {
            flipbook.walking = false;
        }
    }

    public bool GetMouseTarget(out Vector3 target)
    {
        int walkableMask = 1 << NavMesh.GetAreaFromName("Walkable");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.Log(ray.ToString());
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            target = hit.point;
            return true;
        } else {
            target = Vector3.zero;
            return false;
        }
    }
}
