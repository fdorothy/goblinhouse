using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    public CursorType type = CursorType.DEFAULT;
    public bool canClick = true;
    public string title = "";

    void OnMouseEnter()
    {
        if (enabled)
            ClickableManager.singleton.SetCursor(type);
        else
            ClickableManager.singleton.SetCursor(CursorType.NO);
    }

    void OnMouseExit()
    {
        ClickableManager.singleton.ClearCursor();
    }

    private void OnMouseDown()
    {
        if (canClick)
        {
            ClickableManager.singleton.OnClick(this);
        }
    }
}
