using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Investigate : Clickable
{
    public string text;

    public override CursorType GetCursorType()
    {
        return CursorType.EYE;
    }

    public override void TakeAction()
    {
        StoryManager.singleton.QuickText(text);
    }
}
