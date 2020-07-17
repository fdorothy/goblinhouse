using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class Clickable : MonoBehaviour
{
    public string customTitle = "";

    public virtual CursorType GetCursorType()
    {
        return CursorType.DEFAULT;
    }

    public virtual string GetTitle()
    {
        if (customTitle.Length > 0)
            return customTitle;
        else
            return name;
    }

    void OnMouseEnter()
    {
        if (enabled)
            ClickableManager.singleton.SetCursor(GetCursorType(), GetTitle());
        else
            ClickableManager.singleton.SetCursor(CursorType.NO, GetTitle());
    }

    void OnMouseExit()
    {
        ClickableManager.singleton.ClearCursor();
    }

    private void OnMouseDown()
    {
        ClickableManager.singleton.OnClick(this);
    }

    public virtual void TakeAction() { }
}
