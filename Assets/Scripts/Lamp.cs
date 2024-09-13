using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public MeshRenderer lampShade;
    public Light lampLight;

    Material onMaterial;
    public Material offMaterial;
    float onIntensity;
    float dstIntensity;

    public void Start()
    {
        if (lampShade)
            onMaterial = lampShade.materials[1];
        onIntensity = lampLight.intensity;
        dstIntensity = onIntensity;
    }

    public void Update()
    {
        lampLight.intensity = Mathf.MoveTowards(lampLight.intensity, dstIntensity, Time.deltaTime * 20f);
    }

    public void TurnOff()
    {
        //SetLampMaterial(offMaterial);
        dstIntensity = 0f;
    }

    public void TurnOn()
    {
        //SetLampMaterial(onMaterial);
        dstIntensity = onIntensity;
    }

    public void SetLampMaterial(Material mat)
    {
        if (lampShade)
        {
            Material[] materials = lampShade.materials;
            materials[1] = mat;
            lampShade.materials = materials;
        }
    }
}
