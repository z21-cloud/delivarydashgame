using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Package : MonoBehaviour
{
    private void OnEnable()
    {
        Driver.PlayerPackagePickUp += PackagePickedUp;
    }

    private void PackagePickedUp()
    {
        PoolManager.Instance.ReturnPackage(this);
    }

    private void OnDisable()
    {
        Driver.PlayerPackagePickUp -= PackagePickedUp;
    }
}
