using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooling<T> where T : MonoBehaviour
{
    private T prefab;
    private List<T> objects;
    private Transform parent;

    public ObjectPooling(T prefab, int initialSize = 10, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;
        objects = new List<T>(initialSize);

        for (int i = 0; i < initialSize; i++)
        {
            CreateNewObject();
        }

    }

    private T CreateNewObject()
    {
        var obj = GameObject.Instantiate(prefab, parent);
        obj.gameObject.SetActive(false);
        objects.Add(obj);
        return obj;
    }

    public T Get()
    {
        foreach (var obj in objects)
        {
            if(obj != null && !obj.gameObject.activeInHierarchy)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        var newObj = CreateNewObject();
        newObj.gameObject.SetActive(true);
        return newObj;
    }

    public void Release(T obj)
    {
        if (obj == null) return;
        obj.gameObject.SetActive(false);
        if(!objects.Contains(obj))
        {
            objects.Add(obj);
        }
    }

    public void ClearPool()
    {
        foreach (T item in objects)
        {
            if (item.gameObject.activeInHierarchy) item.gameObject.SetActive(false);
        }
    }
}
