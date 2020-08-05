using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Clickable : MonoBehaviour
{
    public string customTitle = "";

    public virtual CursorType GetCursorType()
    {
        return CursorType.DEFAULT;
    }

    public void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("clickables");
    }

    public virtual string GetTitle()
    {
        if (customTitle.Length > 0)
            return customTitle;
        else
            return name;
    }

    public void MouseEnter()
    {
        if (enabled)
            ClickableManager.singleton.SetCursor(GetCursorType(), GetTitle());
        else
            ClickableManager.singleton.SetCursor(CursorType.NO, GetTitle());
    }

    public void MouseExit()
    {
        ClickableManager.singleton.ClearCursor();
    }

    public void MouseDown()
    {
        ClickableManager.singleton.OnClick(this);
    }

    public virtual void TakeAction() { }
}
