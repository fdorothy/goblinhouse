using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHolding : MonoBehaviour
{
    public string itemName;
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
            bool state = (string)StateManager.singleton.content.story.variablesState["holding"] == itemName;
            Show(state ^ invertLogic);
        }
    }

    public void Show(bool visible)
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(visible);
    }
}
