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
        KillTweeners();
        this.text.text = text;
        tweener = panel.DOFade(1.0f, 1.0f);
    }

    public void KillTweeners()
    {
        if (tweener != null)
        {
            tweener.Kill();
            tweener = null;
        }
    }

    public void Shake()
    {
        panel.GetComponent<RectTransform>().DOShakeAnchorPos(0.25f, 10, 100);
    }

    public void ClearText()
    {
        KillTweeners();
        tweener = panel.DOFade(0.0f, 1.0f);
        tweener.OnComplete(() => text.text = "");
    }
}
