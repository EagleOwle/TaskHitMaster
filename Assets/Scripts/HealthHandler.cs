using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    private int _maxHealth = 100;
    private int _health = 100;

    public event EventHandler<float> HealthChanged;

    public bool TakeDamage(int damage)
    {
        if (damage < 0)
        {
            throw new Exception("Attempt to deal negative damage");
        }

        _health -= damage;
        HealthChanged?.Invoke(this, (float)_health / _maxHealth);

        if(_health <= 0)
        {
            Invoke(nameof(Dead), Time.deltaTime);
        }

        return _health <= 0;
    }

    private void Dead()
    {
       // Destroy(gameObject);
    }

}
