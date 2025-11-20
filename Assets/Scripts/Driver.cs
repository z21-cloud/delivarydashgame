using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using NUnit.Framework;

public class Driver : MonoBehaviour
{
    public static event Action PackageEffectsEnable;
    public static event Action PackageEffectsDisable;
    public bool HasPackage { get; private set; }

    private void OnEnable()
    {
        Trap.PlayerFellTrap += DeletePackage;
    }

    private void DeletePackage()
    {
        HasPackage = false;
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

        if(other.GetComponent<JumpData>())
        {
            JumpData jumpData = other.GetComponent<JumpData>();
            Jump carJump = GetComponent<Jump>();
            carJump.CarJump(jumpData.JumpHeightScale, jumpData.JumpPushScale);
        }
    }

    private void OnDisable()
    {
        Trap.PlayerFellTrap -= DeletePackage;
    }
}
