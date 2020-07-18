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
        seeThrough = IsPlaneBetweenPoints(transform.position, transform.forward, Camera.main.transform.position, player.transform.position);
        if (oldSeeThrough != seeThrough)
        {
            if (tweener != null && tweener.IsActive())
                tweener.Kill();
            DoFade();
            oldSeeThrough = seeThrough;
        }
    }

    bool IsPlaneBetweenPoints(Vector3 planeCenter, Vector3 planeNormal, Vector3 p1, Vector3 p2)
    {
        float s1 = Vector3.Dot(p1 - planeCenter, planeNormal);
        float s2 = Vector3.Dot(p2 - planeCenter, planeNormal);
        return !(s1 > 0.0f && s2 > 0.0f || s1 < 0.0f && s2 < 0.0f);
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
