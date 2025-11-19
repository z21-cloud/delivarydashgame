using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [Header("Package Pool")]
    [SerializeField] private Package packagePrefab;
    [SerializeField] private int packagePoolSize = 10;

    [Header("Customer Pool")]
    [SerializeField] private Customer customerPrefab;
    [SerializeField] private int customerPoolSize = 10;

    private ObjectPooling<Package> packagePool;
    private ObjectPooling<Customer> customerPool;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        packagePool = new ObjectPooling<Package>(packagePrefab, packagePoolSize, transform);
        customerPool = new ObjectPooling<Customer>(customerPrefab, customerPoolSize, transform);
    }

    public void SpawnPackage(Vector3 position)
    {
        Package package = packagePool.Get();
        package.transform.position = position;
    }

    public void ReturnPackage(Package package)
    {
        packagePool.Release(package);
    }

    public void SpawnCustomer(Vector3 position)
    {
        Customer customer = customerPool.Get();
        customer.transform.position = position;
    }

    public void ReturnCustomer(Customer customer)
    {
        customerPool.Release(customer);
    }
}
