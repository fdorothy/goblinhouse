﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Investigate : Clickable
{
    public string inkPath = "";

    public override CursorType GetCursorType()
    {
        return CursorType.EYE;
    }

    public override void TakeAction()
    {
        Content content = StateManager.singleton.content;
        if (!content.runningStory)
        {
            content.ProcessInput(inkPath, this);
        }
    }
}
