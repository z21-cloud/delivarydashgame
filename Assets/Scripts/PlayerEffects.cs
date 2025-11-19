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
        Driver.PlayerPackagePickUp += EnableEffect;
        Driver.PlayerPackageDelivered += DisableEffect;
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
        Driver.PlayerPackagePickUp -= EnableEffect;
        Driver.PlayerPackageDelivered -= DisableEffect;
    }
}
