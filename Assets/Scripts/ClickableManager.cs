using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CursorType
{
    EYE, CROSSHAIR, PICKUP, NO, DEFAULT
}

[System.Serializable]
public class CursorVariant
{
    public CursorType type;
    public Vector2 hotSpot = Vector2.zero;
    public Texture2D texture;
}

public class ClickableManager : MonoBehaviour
{
    public CursorVariant[] cursors;
    protected Dictionary<CursorType, CursorVariant> cursorLookup;
    public Clickable lastClicked;
    public Player player;
    public Toast hoverToast;
    public static ClickableManager singleton;

    ClickableManager()
    {
        singleton = this;
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        cursorLookup = new Dictionary<CursorType, CursorVariant>();
        foreach (CursorVariant cursor in cursors)
        {
            cursorLookup[cursor.type] = cursor;
        }
    }

    public void SetCursor(CursorType type, string hoverText)
    {
        if (cursorLookup.ContainsKey(type))
        {
            CursorVariant cursor = cursorLookup[type];
            Cursor.SetCursor(cursor.texture, cursor.hotSpot, CursorMode.ForceSoftware);
            hoverToast.SetText(hoverText);
        }
        else
        {
            ClearCursor();
        }
    }

    public void ClearCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        hoverToast.ClearText();
    }

    public void OnClick(Clickable clickable)
    {
        Debug.Log("clicked on " + clickable.name);
        lastClicked = clickable;
        hoverToast.Shake();
        if (IsCloseEnough())
        {
            TakeAction(lastClicked);
        } else
        {
            Debug.Log("not close enough, moving");
            player.SetDestination(clickable.transform.position);
        }
    }

    public void TakeAction(Clickable clickable)
    {
        Debug.Log("taking action on clickable" + clickable.name);
        lastClicked = null;
        hoverToast.ClearText();
    }

    public void Update()
    {
        if (lastClicked)
        {
            if (IsCloseEnough())
            {
                TakeAction(lastClicked);
            }
        }
    }

    public void ClearLastClicked()
    {
        lastClicked = null;
    }

    public bool IsCloseEnough()
    {
        float dx = player.transform.position.x - lastClicked.transform.position.x;
        float dz = player.transform.position.z - lastClicked.transform.position.z;
        float sqrMag = dx * dx + dz * dz;
        float eps = 5.0f;
        return sqrMag < eps * eps;
    }
}
