using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Update()
    {
        if (Camera.main)
        {
            Vector3 p2 = Camera.main.transform.forward;
            Vector3 dir = new Vector3(-p2.x, 0.0f, -p2.z);
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}