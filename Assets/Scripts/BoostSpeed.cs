using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BoostSpeed : MonoBehaviour
{
    [SerializeField] private float speedBoost = 10f;
    private void OnEnable()
    {
        Driver.PackageBoostEnable += BoostPlayer;
    }

    private void BoostPlayer()
    {
        PlayerLocator.Player.GetComponent<PlayerParameters>().ChangeSpeed(speedBoost);
        Destroy(gameObject); //will add pool
    }

    private void OnDisable()
    {
        Driver.PackageBoostEnable -= BoostPlayer;
    }
}
