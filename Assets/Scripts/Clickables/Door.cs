using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Clickable
{
    public string scene;
    public string entryPoint;

    public override CursorType GetCursorType()
    {
        return CursorType.DOOR;
    }

    public virtual string GetEntryPoint()
    {
        if (entryPoint.Length > 0)
        {
            return entryPoint;
        }
        else
        {
            return "From" + gameObject.scene.name;
        }
    }

    public override void TakeAction()
    {
        StateManager.singleton.LoadScene(scene, GetEntryPoint());
    }
}
