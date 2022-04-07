using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pool
{
    private static List<Part> pools;

    private struct Part
    {
        public string name;
        public List<PoolComponent> prefab;
        public bool resize;
        public Transform parent;
    }

    public static void CreatePool(PoolComponent sample, string name, int count, bool autoResize)
    {
        if (pools == null || count <= 0 || name.Trim() == string.Empty || sample == null) return;

        Part p = new Part();
        p.prefab = new List<PoolComponent>();
        p.name = name;
        p.resize = autoResize;
        p.parent = new GameObject("Pool-" + name).transform;

        for (int i = 0; i < count; i++)
        {
            PoolComponent comp = AddObject(sample, name, i, p.parent);
            p.prefab.Add(comp);
        }

        pools.Add(p);
        Debug.Log(" Добавлен пул: " + name);
    }

    private static PoolComponent AddObject(PoolComponent sample, string name, int index, Transform parent)
    {
        PoolComponent comp = GameObject.Instantiate(sample) as PoolComponent;
        comp.poolName = name;
        comp.gameObject.name = name + "-" + index;
        comp.transform.parent = parent;
        comp.gameObject.SetActive(false);
        return comp;
    }

    private static void AutoResize(Part part, int index)
    {
        PoolComponent comp = AddObject(part.prefab[0], part.name, index, part.parent);
        part.prefab.Add(comp);
    }

    public static PoolComponent GetObject(string name, Vector3 position, Quaternion rotation)
    {
        if (pools == null) return null;

        foreach (Part part in pools)
        {
            if (string.Compare(part.name, name) == 0)
            {
                foreach (PoolComponent comp in part.prefab)
                {
                    if (!comp.isActiveAndEnabled)
                    {
                        comp.transform.rotation = rotation;
                        comp.transform.position = position;
                        comp.gameObject.SetActive(true);

                        return comp;
                    }
                }

                if (part.resize)
                {
                    AutoResize(part, part.prefab.Count);

                    PoolComponent comp = part.prefab[part.prefab.Count - 1];
                    comp.transform.rotation = rotation;
                    comp.transform.position = position;
                    comp.gameObject.SetActive(true);

                    return comp;
                }
            }
        }

        return null;
    }

    public static void ReturnToPool(PoolComponent component)
    {
        foreach (Part part in pools)
        {
            if (string.Compare(part.name, component.poolName) == 0)
            {
                component.transform.position = Vector3.zero;
                component.transform.rotation = Quaternion.identity;
                component.transform.parent = part.parent;
                part.prefab.Add(component);
                component.gameObject.SetActive(false);
            }
        }
    }

    public static void DestroyPool(string name)
    {
        if (pools == null) return;

        int j = 0;

        foreach (Part p in pools)
        {
            if (string.Compare(p.name, name) == 0)
            {
                GameObject.Destroy(p.parent.gameObject);
                pools.RemoveAt(j);
                Debug.Log(" Уничтожен пул: " + name);
                return;
            }

            j++;
        }
    }

    public static void Initialize()
    {
        pools = new List<Part>();
    }

}
