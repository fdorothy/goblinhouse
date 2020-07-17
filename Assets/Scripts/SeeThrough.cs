using UnityEngine;

public class SeeThrough : MonoBehaviour
{
    protected MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        Vector3 cam = new Vector3(Camera.main.transform.forward.x, 0.0f, Camera.main.transform.forward.z);
        cam.Normalize();
        float angle = Vector3.Angle(cam, -transform.forward);
        bool enabled = angle < 90.0f;
        if (enabled)
        {
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
        else
        {
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        }
    }
}
