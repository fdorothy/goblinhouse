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
    public static float lifetime = 3.0f;
    public static float fadetime = 2.0f;
    public static float shiftSpeed = 0.5f;
    public bool destroying = false;

    private void Start()
    {
        panel.alpha = 0.0f;
    }

    public void RunDialogue(string text)
    {
        this.text.text = text;
        Sequence seq = DOTween.Sequence();
        seq.Append(panel.DOFade(1.0f, fadetime));
    }

    public void DestroyDialogue(System.Action onDone)
    {
        if (destroying)
            return;
        destroying = true;
        Sequence seq = DOTween.Sequence();
        seq.Append(panel.DOFade(0.0f, fadetime));
        seq.OnComplete(() =>
        {
            onDone.Invoke();
            Destroy(this.gameObject);
        });
    }

    public void Shift()
    {
        if (shifter != null && shifter.IsActive())
        {
            shifter.Kill(true);
            shifter = null;
        }
        shifter = transform.DOMoveY(transform.position.y + GetHeight(), shiftSpeed, false);
    }

    public float GetHeight()
    {
        return this.GetComponent<RectTransform>().rect.height;
    }
}
