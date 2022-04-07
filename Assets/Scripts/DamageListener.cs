using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageListener : MonoBehaviour, IDamageTaker
{
    private HealthHandler _healthHandler;

    private void Awake()
    {
        _healthHandler = GetComponentInParent<HealthHandler>();

        if(_healthHandler == null)
        {
            Debug.LogError("HealthHandler is null");
        }
    }

    bool IDamageTaker.TakeDamage(int damage)
    {
       return _healthHandler.TakeDamage(damage);
    }
}
