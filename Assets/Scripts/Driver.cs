using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using NUnit.Framework;

public class Driver : MonoBehaviour
{
    public static event Action PackageEffectsEnable;
    public static event Action PackageEffectsDisable;
    public static event Action PackageBoostEnable;
    public bool HasPackage { get; private set; }
    public bool BoostPackage { get; private set; }

    private float currentSpeed;

    private void OnEnable()
    {
        Trap.PlayerFellTrap += DeletePackage;
        EnemyController.EnemyDeliveredPackage += DeletePackage;
    }

    private void DeletePackage()
    {
        HasPackage = false;
        BoostPackage = false;
        PackageEffectsDisable?.Invoke();   //  передать информацию клиенту, что пакет передался
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Package>() && !HasPackage)
        {
            HasPackage = true;              //  подобрать пакет
            PackageEffectsEnable?.Invoke();  //  передать информацию пакету, что он подобран
            //  передать информацию менеджеру, что игрок подобрал пакет (для спауна противников)
            //  воспроизвести эффект и анимацию
        }

        if(other.GetComponent<Customer>() && HasPackage)
        {
            HasPackage = false;
            //  воспроизвести эффект и анимаацию
            PackageEffectsDisable?.Invoke();   //  передать информацию клиенту, что пакет передался
        }

        if(other.GetComponent<BoostSpeed>() && !BoostPackage)
        {
            BoostPackage = true;
            Debug.Log("boosted");
            PackageBoostEnable?.Invoke();
        }

        if (other.GetComponent<JumpData>())
        {
            JumpData jumpData = other.GetComponent<JumpData>();
            Jump carJump = GetComponent<Jump>();
            carJump.CarJump(jumpData.JumpHeightScale, jumpData.JumpPushScale);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (BoostPackage)
        {
            BoostPackage = false;
            Debug.Log("deboosted");
            PlayerLocator.Player.GetComponent<PlayerParameters>().ChangeSpeed(5f); // dont won't to make another component for deboost. Can't add it to boost speed, because it destroys (backs to pool)
        }
    }

    private void OnDisable()
    {
        Trap.PlayerFellTrap -= DeletePackage;
        EnemyController.EnemyDeliveredPackage -= DeletePackage;
    }
}
