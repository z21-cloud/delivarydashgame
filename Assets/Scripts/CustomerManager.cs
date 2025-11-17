using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private List<Customer> customers;
    private int index;
    private void Start()
    {
        customers = FindObjectsByType<Customer>(FindObjectsSortMode.None).ToList();
        index = 0;
    }
}
