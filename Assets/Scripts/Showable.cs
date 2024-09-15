using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Hides the children of this gameobject if
 * the content's variable is true.
 */
public class Showable : MonoBehaviour
{
    public string varName;
    public bool invertLogic = false;

    // Start is called before the first frame update
    void Start()
    {
        Show(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (StateManager.singleton && StateManager.singleton.content)
        {
            bool state = (int)StateManager.singleton.content.story.variablesState[varName] != 0;
            Show(state ^ invertLogic);
        }
    }

    public void Show(bool visible)
    {
        for (int i=0; i<transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(visible);
    }
}
