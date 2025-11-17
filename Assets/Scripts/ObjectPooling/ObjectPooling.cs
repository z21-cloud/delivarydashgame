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
        var obj = GameObject.Instantiate(prefab);
        obj.gameObject.SetActive(false);
        objects.Add(obj);
        return obj;
    }

    public T Get()
    {
        T obj = null;

        for (int i = objects.Count - 1; i >= 0; i--)
        {
            if (objects[i] != null)
            {
                obj = objects[i];
                objects.RemoveAt(i);
                break;
            }
            else
            {
                objects.RemoveAt(i);
            }
        }

        if(obj == null)
        {
            obj = CreateNewObject();
            objects.RemoveAt(objects.Count - 1);
        }

        obj.gameObject.SetActive(true);
        return obj;
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
}
