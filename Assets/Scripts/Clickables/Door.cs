using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Clickable
{
    public override CursorType GetCursorType()
    {
        return CursorType.DOOR;
    }

    public override void TakeAction()
    {
        Content content = StateManager.singleton.content;
        if (!content.runningStory)
        {
            content.ProcessInput(name, this);
        }
    }
}
