using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolComponent : MonoBehaviour
{
    public string poolName;

    public void ReturnToPool()
    {
        Pool.ReturnToPool(this);
    }

}
