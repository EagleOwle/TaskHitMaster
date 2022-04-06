using System;

public interface IDamageTaker
{
    public bool TakeDamage(int damage);
    public event EventHandler<float> HealthChanged;
}