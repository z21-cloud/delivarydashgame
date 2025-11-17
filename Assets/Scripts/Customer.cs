using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Customer : MonoBehaviour
{
    private void OnEnable()
    {
        Driver.PlayerPackageDelivered += PackageDelivered;        
    }

    private void PackageDelivered()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        Driver.PlayerPackageDelivered -= PackageDelivered;
    }
}
