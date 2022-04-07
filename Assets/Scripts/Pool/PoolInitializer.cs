using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolInitializer : MonoBehaviour
{
    [SerializeField] private PoolComponent projectileprefab;

    void Awake()
    {
        Pool.Initialize();
        Pool.CreatePool(projectileprefab, "Projectile", 1, true);
    }
}
