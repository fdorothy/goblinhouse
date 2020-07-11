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
                SetDestination(target);
                ClickableManager.singleton.ClearLastClicked();
            }
        }

        if (agent.hasPath)
        {
            flipbook.CalculateFacing(agent.velocity);
            flipbook.walking = true;
        }
        else
        {
            flipbook.walking = false;
        }
    }

    public void SetDestination(Vector3 target)
    {
        agent.SetDestination(target);
    }

    public bool GetMouseTarget(out Vector3 target)
    {
        int walkableMask = 1 << NavMesh.GetAreaFromName("Walkable");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f))
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
