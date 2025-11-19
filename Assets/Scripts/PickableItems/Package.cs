using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Package : MonoBehaviour
{
    private void OnEnable()
    {
        Driver.PackageEffectsEnable += PackagePickedUp;
    }

    private void PackagePickedUp()
    {
        PoolManager.Instance.Return(this);
    }

    private void OnDisable()
    {
        Driver.PackageEffectsEnable -= PackagePickedUp;
    }
}
