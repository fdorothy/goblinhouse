using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CursorType
{
    EYE, CROSSHAIR, DOOR, NO, DEFAULT
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

    protected int clickablesLayerMask;

    ClickableManager()
    {
        singleton = this;
    }
    
    void Start()
    {
        clickablesLayerMask = LayerMask.GetMask(new string[] { "clickables" });
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

    public bool IsHovering()
    {
        return lastClicked != null;
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
        }
        else
        {
            player.SetDestination(clickable.transform.position);
        }
    }

    public void TakeAction(Clickable clickable)
    {
        Debug.Log("taking action on clickable " + clickable.name);
        lastClicked = null;
        hoverToast.ClearText();
        clickable.TakeAction();
    }

    void Update()
    {
        // if we are running a story, then no clicks!
        if (StateManager.singleton.content.runningStory)
        {
            return;
        }

        if (lastClicked != null)
        {
            if (IsCloseEnough())
            {
                TakeAction(lastClicked);
            }
        }
        CheckHover();

        if (Input.GetMouseButtonDown(0) && !ClickableManager.singleton.IsHovering())
        {
            RaycastHit hit;
            bool didHit = GetMouseTarget(out hit, clickablesLayerMask);
            bool hitClickable = true;
            if (!didHit)
            {
                hitClickable = false;
                didHit = GetMouseTarget(out hit);
            }
            if (didHit)
            {
                if (hitClickable)
                {
                    Transform t = hit.collider.transform;
                    if (t.GetComponent<Clickable>() != null)
                    {
                        Clickable clickable = t.GetComponent<Clickable>();
                        clickable.MouseDown();
                    }
                }
                else
                {
                    player.SetDestination(hit.point);
                    ClearLastClicked();
                }
            }
        }
    }

    public void CheckHover()
    {
        RaycastHit hit;
        if (GetMouseTarget(out hit, clickablesLayerMask))
        {
            Transform t = hit.collider.transform;
            if (t.GetComponent<Clickable>() != null)
            {
                Clickable clickable = t.GetComponent<Clickable>();
                clickable.MouseEnter();
            }
            else
            {
                ClearCursor();
            }
        } else
        {
            ClearCursor();
        }
    }

    public bool GetMouseTarget(out RaycastHit hit, int layerMask = 0)
    {
        float fixedRatio = 320.0f / 240.0f;
        float w = Screen.width;
        float h = Screen.height;
        float screenRatio = w / h;

        float rw = w, rh = h, offsetY = 0.0f, offsetX = 0.0f;
        if (screenRatio > fixedRatio)
        {
            // we have a narrow screen, there is no padding and the left and right sides stick out
            rw = h * fixedRatio;
            rh = h;
        } else
        {
            rw = w;
            rh = w / fixedRatio;
        }
        offsetY = (h - rh) / 2.0f;
        offsetX = (w - rw) / 2.0f;

        float mx = Input.mousePosition.x;
        float my = Input.mousePosition.y;
        Vector3 v = new Vector3((mx - offsetX) / rw, (my - offsetY) / rh);
        Ray ray = Camera.main.ViewportPointToRay(v);
        if (layerMask != 0)
        {
            return Physics.Raycast(ray, out hit, 1000.0f, layerMask);
        } else
        {
            return Physics.Raycast(ray, out hit, 1000.0f);
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
