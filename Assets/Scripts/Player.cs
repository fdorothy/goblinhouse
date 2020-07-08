using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpriteLoop

public class Player : MonoBehaviour
{
    protected NavMeshAgent agent;
    public SpriteRenderer sr;
    public Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse button down");
            Vector3 target;
            if (GetMouseTarget(out target))
            {
                Debug.Log("got target");
                agent.SetDestination(target);
            }
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
            Debug.Log("got hit");
            target = hit.point;
            return true;
        } else {
            target = Vector3.zero;
            return false;
        }
    }
}
