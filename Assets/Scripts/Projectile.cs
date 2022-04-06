using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _lifetime = 10f;
    [SerializeField] private float _projectileFlightDuration = 1f;
    [SerializeField] protected LayerMask _targetLayer = 0;
    [SerializeField] private LayerMask _environmentLayer = 0;

    [HideInInspector]public Vector3 targetPosition;

    private Rigidbody _rigidbody;
    private Collider _collider;

    private bool _hasHit;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public void Launch()
    {
        Vector3 velocity = Ballistik.CalculateBestThrowSpeed(transform.position, targetPosition, _projectileFlightDuration);
        Invoke(nameof(SelfDestroy), _lifetime);
        transform.parent = null;
        _collider.enabled = true;
        _rigidbody.isKinematic = false;
        _rigidbody.velocity = velocity;
        _hasHit = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (_hasHit == true) return;

        //Debug.LogError("OnTriggerEnter");
        if ((1 << collider.gameObject.layer & _targetLayer) != 0)
        {
            HitToTarget(collider.gameObject);
        }
        else if ((1 << collider.gameObject.layer & _environmentLayer) != 0)
        {
            HitToEnvironment(collider.gameObject);
        }
    }

    private void HitToEnvironment(GameObject target)
    {
        
    }

    private void HitToTarget(GameObject target)
    {
        if (_hasHit == true) return;

        if (target.TryGetComponent(out IDamageTaker damageTaker))
        {
            _hasHit = true;

            if (damageTaker.TakeDamage(int.MaxValue) == false)
            {
                
            }
        }
        else
        {
            Debug.LogError("Cant find IDamageTaker component on " + target.name);
        }
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }

}
