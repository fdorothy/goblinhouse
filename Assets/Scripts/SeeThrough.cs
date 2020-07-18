using UnityEngine;
using DG.Tweening;

public class SeeThrough : MonoBehaviour
{
    protected MeshRenderer meshRenderer;
    protected Player player;
    protected bool seeThrough = false;
    protected float upPosition;
    protected float downPosition;
    protected bool oldSeeThrough = false;
    protected Tweener tweener;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        player = FindObjectOfType<Player>();
        upPosition = transform.position.y;
        downPosition = upPosition - 9;
    }

    void Update()
    {
        float s1 = Vector3.Dot(player.transform.position - this.transform.position, this.transform.forward);
        float s2 = Vector3.Dot(Camera.main.transform.position - this.transform.position, this.transform.forward);
        seeThrough = !(s1 > 0.0f && s2 > 0.0f || s1 < 0.0f && s2 < 0.0f);
        if (oldSeeThrough != seeThrough)
        {
            if (tweener != null && tweener.IsActive())
                tweener.Kill();
            DoFade();
            oldSeeThrough = seeThrough;
        }
    }

    void DoMovement()
    {
        const float delta = 1.0f;
        const float t = 2.0f;
        if (seeThrough)
            tweener = transform.DOMoveY(downPosition, Random.Range(t - delta / 2, t + delta / 2));
        else
            tweener = transform.DOMoveY(upPosition, Random.Range(t - delta / 2, t + delta / 2));
    }

    void DoFade()
    {
        if (seeThrough)
        {
            tweener = meshRenderer.material.DOFade(0.6f, 1.0f);
        } else
        {
            tweener = meshRenderer.material.DOFade(1.0f, 1.0f);
        }
    }
}
