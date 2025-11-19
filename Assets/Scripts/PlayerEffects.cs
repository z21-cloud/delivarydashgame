using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerEffects : MonoBehaviour
{
    ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        Driver.PackageEffectsEnable += EnableEffect;
        Driver.PackageEffectsDisable += DisableEffect;
    }

    private void EnableEffect()
    {
        particle.Play();
    }

    private void DisableEffect()
    {
        particle.Stop();
    }

    private void OnDisable()
    {
        Driver.PackageEffectsEnable -= EnableEffect;
        Driver.PackageEffectsDisable -= DisableEffect;
    }
}
