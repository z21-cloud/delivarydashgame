using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Customer : MonoBehaviour
{
    private void OnEnable()
    {
        Driver.PackageEffectsDisable += PackageDelivered;        
    }

    private void PackageDelivered()
    {
        PoolManager.Instance.Return(this);
    }

    private void OnDisable()
    {
        Driver.PackageEffectsDisable -= PackageDelivered;
    }
}
