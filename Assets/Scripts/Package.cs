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
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        Driver.PlayerPackagePickUp -= PackagePickedUp;
    }
}
