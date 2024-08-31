using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueChoice : MonoBehaviour
{
    public Text text;
    System.Action callback;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RunChoice(string text, System.Action callback)
    {
        this.text.text = text;
        this.callback = callback;
    }

    public void OnClick()
    {
        callback.Invoke();
    }
}
