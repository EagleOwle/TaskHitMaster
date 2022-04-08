using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _lifetime = 10f;
    [SerializeField] private float _projectileFlightDuration = 1f;
    [SerializeField] protected LayerMask _targetLayer = 0;
    [SerializeField] private LayerMask _environmentLayer = 0;

    private Rigidbody _rigidbody;
    private Collider _collider;

    private bool _hasHit;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        _collider.enabled = false;
        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector3.zero;
        _hasHit = true;
    }

    public void Launch(Vector3 shootPosition)
    {
        Vector3 velocity = Ballistik.CalculateBestThrowSpeed(transform.position, shootPosition, _projectileFlightDuration);
        Invoke(nameof(SelfDestroy), _lifetime);
        transform.parent = null;
        _collider.enabled = true;
        _rigidbody.isKinematic = false;
        _rigidbody.velocity = velocity;
        _hasHit = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasHit == true) return;

        if ((1 << collision.collider.gameObject.layer & _targetLayer) != 0)
        {
            HitToTarget(collision.collider.gameObject);

            collision.collider.gameObject.AddComponent<PushMarker>();
        }
        else
        {
            if ((1 << collision.collider.gameObject.layer & _environmentLayer) != 0)
            {
                HitToEnvironment(collision.collider.gameObject);
            }
        }
    }

    private void HitToEnvironment(GameObject target)
    {
        _hasHit = true;
        SelfDestroy();
    }

    private void HitToTarget(GameObject target)
    {
        if (_hasHit == true) return;

        _hasHit = true;

        if (target.TryGetComponent(out IDamageTaker damageTaker))
        {
            damageTaker.TakeDamage(100);
        }

        SelfDestroy();
    }

    

    private void SelfDestroy()
    {
        //Destroy(gameObject);
        CancelInvoke(nameof(SelfDestroy));
        GetComponent<PoolComponent>().ReturnToPool();
    }

}
