using UnityEngine;

public class PoolInitializer : MonoBehaviour
{
    [SerializeField] private PoolInitialisator[] poolInitialisators;

    void Awake()
    {
        Pool.Initialize();

        foreach (var item in poolInitialisators)
        {
            Pool.CreatePool(item.poolComponentPrefab, item.poolName, 1, true);
        }

        
    }

    [System.Serializable]
    public class PoolInitialisator
    {
        public string poolName;
        public PoolComponent poolComponentPrefab;
        public int poolCount = 1;
        public bool autoSize = false;
    }
    
}
