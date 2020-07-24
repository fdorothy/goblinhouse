using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Dialogue : MonoBehaviour
{
    public Text text;
    public CanvasGroup panel;
    public Tweener shifter;

    private void Start()
    {
        panel.alpha = 0.0f;
    }

    public void RunDialogue(string text, System.Action onDone)
    {
        this.text.text = text;
        Sequence seq = DOTween.Sequence();
        seq.Append(panel.DOFade(1.0f, 0.25f));
        seq.AppendInterval(1.0f);
        seq.Append(panel.DOFade(0.0f, 0.25f));
        seq.OnComplete(() =>
        {
            onDone.Invoke();
            Destroy(this.gameObject);
        });
    }

    public void ShiftDown()
    {
        if (shifter != null && shifter.IsActive())
        {
            shifter.Kill(true);
            shifter = null;
        }
        shifter = transform.DOMoveY(transform.position.y - 40.0f, 0.1f, false);
    }
}
