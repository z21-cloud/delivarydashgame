using UnityEngine;
using System.Collections.Generic;

public class SpawnPoolManager : MonoBehaviour
{
    [SerializeField] private List<Transform> customersNodes;
    [SerializeField] private List<Transform> packagesNodes;
    [SerializeField] private List<Transform> trapNodes;
    [SerializeField] private int trapsCount = 3;
    private List<Transform> tempCustomers;
    private List<Transform> tempPackages;
    private List<Transform> tempTraps;
    private void OnEnable()
    {
        Driver.PackageEffectsDisable += SpawnObjects;
    }

    private void OnDisable()
    {
        Driver.PackageEffectsDisable -= SpawnObjects;
    }

    private void Start()
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        Vector3 packagerPosition = GetRandomPosition(packagesNodes, tempPackages);
        Vector3 customerPosition = GetRandomPosition(customersNodes, tempCustomers);
        PoolManager.Instance.Spawn<Package>(packagerPosition);
        PoolManager.Instance.Spawn<Customer>(customerPosition);

        PoolManager.Instance.ClearPool<Trap>();
        for (int i = 0; i < trapsCount; i++)
        {
            Vector3 trapPosition = GetRandomPosition(trapNodes, tempTraps);
            PoolManager.Instance.Spawn<Trap>(trapPosition);
        }
    }

    private Vector3 GetRandomPosition(List<Transform> nodes, List<Transform> tempNodes)
    {
        if (tempNodes == null || tempNodes.Count == 0)
        {
            Debug.LogWarning($"{nodes}'s spawn point are null");
            tempNodes = new List<Transform>(nodes);
        }

        if (tempNodes.Count > 0)
        {
            int index = Random.Range(0, tempNodes.Count);
            if (tempNodes[index] != null)
            {
                Vector3 position = tempNodes[index].position;
                tempNodes.RemoveAt(index);
                return position;
            }
        }

        Debug.LogWarning("No spawn points");
        return Vector3.zero;
    }

    /*private Vector3 GetRandomTrapSpawnPosition()
    {
        if (tempTraps == null || tempTraps.Count == 0)
        {
            Debug.LogWarning("Customer's spawn points are null");
            tempTraps = new List<Transform>(trapNodes);
        }

        if (tempTraps.Count > 0)
        {
            int index = Random.Range(0, tempTraps.Count);
            if (tempTraps[index] != null)
            {
                Vector3 position = tempTraps[index].position;
                tempTraps.RemoveAt(index);
                return position;
            }
        }
        Debug.LogWarning("No traps spawn points");
        return Vector3.zero;
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
    }*/
}
