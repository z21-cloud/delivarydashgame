using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Trap : MonoBehaviour
{
    public static event Action PlayerFellTrap;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Driver>())
        {
            Driver player = other.GetComponent<Driver>();
            if(player.HasPackage)
                PlayerFellTrap?.Invoke();
        }
    }
}
