using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public CharacterFlipbook flipbook;
    public Vector3 forward = Vector3.forward;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Player player = FindObjectOfType<Player>();
        if (player)
        {
            forward = player.transform.position - transform.position;
            forward = forward.normalized;
        }
        flipbook.CalculateFacing(forward);
    }
}
