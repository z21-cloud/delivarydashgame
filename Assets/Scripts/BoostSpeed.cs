using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BoostSpeed : MonoBehaviour
{
    [SerializeField] private float speedBoost = 10f;
    [SerializeField] private float normalSpeed = 5f;
    private void OnEnable()
    {
        Driver.PackageBoostEnable += BoostPlayer;
        Driver.PackageBoostDisable += UnBoostPlayer;
    }

    private void BoostPlayer()
    {
        PlayerLocator.Player.GetComponent<PlayerParameters>().ChangeSpeed(speedBoost);
    }

    private void UnBoostPlayer()
    {
        PlayerLocator.Player.GetComponent<PlayerParameters>().ChangeSpeed(normalSpeed);
    }

    private void OnDisable()
    {
        Driver.PackageBoostEnable -= BoostPlayer;
        Driver.PackageBoostDisable -= UnBoostPlayer;
    }
}
