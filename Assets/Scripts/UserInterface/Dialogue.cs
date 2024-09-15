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
    public Vector3 targetPosition = Vector3.zero;

    private void Start()
    {
        panel.alpha = 0.0f;
        targetPosition = transform.position;
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

    public void Shift(bool smooth)
    {
        if (shifter != null && shifter.IsActive())
        {
            shifter.Kill(true);
            shifter = null;
            transform.position = targetPosition;
        }
        float targetY = transform.position.y + GetHeight();
        targetPosition = new Vector3(transform.position.x, targetY, transform.position.z);
        if (smooth)
            shifter = transform.DOMove(targetPosition, shiftSpeed, false);
        else
            transform.position = targetPosition;
    }

    public float GetHeight()
    {
        //return this.GetComponent<RectTransform>().rect.height;
        float scale = transform.GetComponentInParent<UnityEngine.UI.CanvasScaler>().scaleFactor;
        return 40 * scale;
    }
}
