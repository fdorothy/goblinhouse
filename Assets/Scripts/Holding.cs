using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holding : MonoBehaviour
{
    public UnityEngine.UI.Text text;
    public UnityEngine.CanvasGroup canvasGroup;
    float targetAlpha = 0.0f;

    public void Start()
    {
        canvasGroup.alpha = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Content content = FindObjectOfType<Content>();
        string holding = content.story.variablesState["holding"].ToString();
        if (holding == "")
        {
            Show(false);
        } else
        {
            Show(true);
            text.text = "holding: " + holding;
        }

        canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, Time.unscaledDeltaTime);
    }

    void Show(bool visible)
    {
        targetAlpha = visible ? 1.0f : 0.0f;
    }
}
