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

    public static System.Action<Vector3> CustomerSpawned; 
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
        
        CustomerSpawned?.Invoke(customerPosition);
        
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
}
