using UnityEngine;
using System.Collections.Generic;

public class SpawnPoolManager : MonoBehaviour
{
    [SerializeField] private List<Transform> customersNodes;
    [SerializeField] private List<Transform> packagesNodes;
    private int index = 0;
    private List<Transform> tempCustomers;
    private List<Transform> tempPackages;
    private void OnEnable()
    {
        Driver.PlayerPackageDelivered += SpawnPackageAndCustomer;
    }

    private void OnDisable()
    {
        Driver.PlayerPackageDelivered -= SpawnPackageAndCustomer;
    }

    private void SpawnPackageAndCustomer()
    {
        Vector3 packagerPosition = GetRandomPackageSpawnPosition();
        Vector3 customerPosition = GetRandomCustomerSpawnPosition();
        PoolManager.Instance.SpawnPackage(packagerPosition);
        PoolManager.Instance.SpawnCustomer(customerPosition);
    }

    private Vector3 GetRandomCustomerSpawnPosition()
    {
        if(tempCustomers == null || tempCustomers.Count == 0)
        {
            Debug.LogWarning("Customer's spawn points are null");
            tempCustomers = new List<Transform>(customersNodes);
        }

        if (tempCustomers.Count > 0)
        {
            int index = Random.Range(0, tempCustomers.Count);
            if (tempCustomers[index] != null)
            {
                Vector3 position = tempCustomers[index].position;
                tempCustomers.RemoveAt(index);
                return position;
            }
        }

        Debug.LogWarning("No customer spawn points");
        return new Vector3(0, 0);
    }

    private Vector3 GetRandomPackageSpawnPosition()
    {
        if (tempPackages == null || tempPackages.Count == 0)
        {
            Debug.LogWarning("Customer's spawn points are null");
            tempPackages = new List<Transform>(packagesNodes);
        }

        if (tempPackages.Count > 0)
        {
            int index = Random.Range(0, tempPackages.Count);
            if (tempPackages[index] != null)
            {
                Vector3 position = tempPackages[index].position;
                tempPackages.RemoveAt(index);
                return position;
            }
        }

        Debug.LogWarning("No package spawn points");
        return new Vector3(0, 0);
    }
}
