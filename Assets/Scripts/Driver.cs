using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Driver : MonoBehaviour
{
    public static event Action PlayerPackagePickUp;
    public static event Action PlayerPackageDelivered;
    private bool hasPackage = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Package>())
        {
            hasPackage = true;              //  подобрать пакет
            PlayerPackagePickUp?.Invoke();  //  передать информацию пакету, что он подобран
            //  передать информацию менеджеру, что игрок подобрал пакет (для спауна противников)
            //  воспроизвести эффект и анимацию
        }

        if(other.GetComponent<Customer>() && hasPackage)
        {
            hasPackage = false;
            //  воспроизвести эффект и анимаацию
            PlayerPackageDelivered?.Invoke();   //  передать информацию клиенту, что пакет передался
        }
    }
}
