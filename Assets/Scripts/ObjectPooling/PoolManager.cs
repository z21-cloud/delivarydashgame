using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [SerializeField] private Package packagePrefab;
    [SerializeField] private Customer customerPrefab;

    private ObjectPooling<Package> packagePool;
    private ObjectPooling<Customer> customerPool;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Instance = null;
        }
        Instance = this;
    }

    private void Start()
    {
        packagePool = new ObjectPooling<Package>(packagePrefab, 10, transform);
        customerPool = new ObjectPooling<Customer>(customerPrefab, 10, transform);
    }

    public void SpawnPackages(Vector3 position)
    {
        Package package = packagePool.Get();
        package.transform.position = position;
    }

    public void ReturnPackage(Package package)
    {
        packagePool.Release(package);
    }
}
