using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Toast : MonoBehaviour
{
    public Text text;
    public CanvasGroup panel;
    public Tweener tweener;

    private void Start()
    {
        panel.alpha = 0.0f;
        this.text.text = "";
    }

    public void SetText(string text)
    {
        this.text.text = text;
        PushTween(() => tweener = panel.DOFade(1.0f, 0.25f));
    }

    public void PushTween(TweenCallback action)
    {
        if (tweener != null && tweener.IsPlaying())
        {
            tweener.OnComplete(action);
        } else
        {
            action.Invoke();
        }
    }

    public void Shake()
    {
        PushTween(() => tweener = panel.GetComponent<RectTransform>().DOPunchAnchorPos(new Vector2(0, 5), 0.25f, 0));
    }

    public void ClearText()
    {
        PushTween(() => tweener = panel.DOFade(0.0f, 0.25f).OnComplete(() => text.text = ""));
    }
}
