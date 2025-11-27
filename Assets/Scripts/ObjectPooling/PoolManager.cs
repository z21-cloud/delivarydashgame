using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [Header("Package Pool")]
    [SerializeField] private Package packagePrefab;
    [SerializeField] private int packagePoolSize = 10;

    [Header("Customer Pool")]
    [SerializeField] private Customer customerPrefab;
    [SerializeField] private int customerPoolSize = 10;

    [Header("Traps Pool")]
    [SerializeField] private Trap trapPrefab;
    [SerializeField] private int trapPoolSize = 6;

    private Dictionary<Type, object> pools = new Dictionary<Type, object>();

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
        pools[typeof(Package)] = new ObjectPooling<Package>(packagePrefab, packagePoolSize, transform);
        pools[typeof(Customer)] = new ObjectPooling<Customer>(customerPrefab, customerPoolSize, transform);
        pools[typeof(Trap)] = new ObjectPooling<Trap>(trapPrefab, trapPoolSize, transform);
    }

    public T Spawn<T>(Vector3 position) where T : MonoBehaviour
    {
        var pool = GetPool<T>();
        T obj = pool.Get();
        obj.transform.position = position;
        return obj;
    }

    public void Return<T>(T obj) where T : MonoBehaviour
    {
        var pool = GetPool<T>();
        pool.Release(obj);
    }

    public void ClearPool<T>() where T : MonoBehaviour
    {
        var pool = GetPool<T>();
        pool.ClearPool();
    }

    private ObjectPooling<T> GetPool<T>() where T : MonoBehaviour
    {
        if(pools.TryGetValue(typeof(T), out object poolType))
        {
            return poolType as ObjectPooling<T>;
        }

        return null;
    }

    /*public void ReturnPackage(Package package)
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

    public void SpawnTraps(Vector3 position)
    {
        Trap trap = trapPool.Get();
        trap.transform.position = position;
    }

    public void ReturnTrap(Trap trap)
    {
        trapPool.Release(trap);
    }

    public void ClearAllTraps()
    {
        trapPool.ClearPool();
    }*/
}
