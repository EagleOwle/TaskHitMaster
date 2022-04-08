using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageListener : MonoBehaviour, IDamageTaker
{
    public enum DamagePower { None, Part, Half, Max }
    [SerializeField] private DamagePower damagePower;

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
        if (_healthHandler == null) return false;

        switch (damagePower)
        {
            case DamagePower.None:
                break;
            case DamagePower.Part:
                damage = damage / 4;
                break;
            case DamagePower.Half:
                damage = damage / 2;
                break;
            case DamagePower.Max:
                break;
        }

        return _healthHandler.TakeDamage(damage);
    }

}
