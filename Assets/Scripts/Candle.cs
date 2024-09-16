using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    public MeshRenderer flame;
    protected Material mat;
    protected Vector4 emissionColor;
    protected float t = 0.0f;
    float target = 1.0f;
    float intensity = 1.0f;
    Vector3 startPosition = Vector3.zero;
    float timeOffset = 0.0f;

    public void Start()
    {
        mat = flame.materials[0];
        startPosition = flame.transform.position;
        timeOffset = Random.Range(0f, 10f);
        emissionColor = mat.GetColor("_EmissionColor");
        StartCoroutine(FlickerRoutine());
    }

    IEnumerator FlickerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 6f));
            for (int i=0; i<4; i++)
            {
                yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
                target = 0.0f;
                yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
                target = 1.0f;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        intensity = Mathf.MoveTowards(intensity, target, Time.unscaledDeltaTime * 20f);
        mat.SetColor("_EmissionColor", emissionColor * intensity);

        float offset = 0.2f * Mathf.Sin(Time.unscaledTime * 5f + timeOffset);
        flame.transform.position = startPosition + Vector3.up * offset;
    }
}
