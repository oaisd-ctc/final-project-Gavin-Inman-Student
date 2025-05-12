using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Torch : MonoBehaviour
{
    void Start()
    {
        Animator torch = GetComponent<Animator>();
        torch.Play("Torch", 0, Random.value);
    }

    void Update()
    {
            Flicker();
    }

    void Flicker()
    {
        Light2D torch = GetComponent<Light2D>();

        float intensity;
        float radius;

        intensity = Random.Range(1.5f, 2);
        radius = Random.Range(5.5f, 6);

        torch.intensity = intensity;
        torch.pointLightOuterRadius = radius;
    }
}
