using UnityEngine;
using DG.Tweening;

public class SeeThrough : MonoBehaviour
{
    protected MeshRenderer meshRenderer;
    protected Player player;
    protected bool seeThrough = false;
    protected bool oldSeeThrough = false;
    public bool isPoint = false;
    protected Tweener tweener;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (player == null || meshRenderer == null)
        {
            FindPlayer();
            return;
        }
        Vector3 normal = isPoint ? Camera.main.transform.forward : transform.forward;
        seeThrough = IsPlaneBetweenPoints(transform.position, normal, Camera.main.transform.position, player.transform.position);
        if (oldSeeThrough != seeThrough)
        {
            if (tweener != null && tweener.IsActive())
                tweener.Kill();
            DoFade();
            oldSeeThrough = seeThrough;
        }
    }

    void FindPlayer()
    {
        player = FindObjectOfType<Player>();
    }

    bool IsPlaneBetweenPoints(Vector3 planeCenter, Vector3 planeNormal, Vector3 p1, Vector3 p2)
    {
        float s1 = Vector3.Dot(p1 - planeCenter, planeNormal);
        float s2 = Vector3.Dot(p2 - planeCenter, planeNormal);
        return !(s1 > 0.0f && s2 > 0.0f || s1 < 0.0f && s2 < 0.0f);
    }

    void DoFade()
    {
        if (seeThrough)
        {
            meshRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            //tweener = meshRenderer.material.DOFade(0.0f, 1.0f);
        } else
        {
            meshRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            //tweener = meshRenderer.material.DOFade(1.0f, 1.0f);
        }
    }
}
