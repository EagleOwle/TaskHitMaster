using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Action<Enemy> actionIsDead;

    [SerializeField] private float _lookSpeed = 10;

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private HealthHandler _healthHandler;
    private UnitState _currentUnitState;

    private Vector3 _lookTarget;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _healthHandler = GetComponent<HealthHandler>();
    }

    private void OnEnable()
    {
       _healthHandler.HealthChanged += OnHealthChanged;
        DasableRagdoll();
    }

    private void OnHealthChanged(object sender, float e)
    {
        if (e <= 0)
        {
            Destroy(_navMeshAgent);
            Destroy(_animator);
            Destroy(_healthHandler);
            actionIsDead?.Invoke(this);
            EnableRagdoll();
            Destroy(this);
        }
    }

    private void OnDisable()
    {
        GetComponent<HealthHandler>().HealthChanged -= OnHealthChanged;
    }

    public void StartHunt()
    {
        _currentUnitState = UnitState.Move;
        _navMeshAgent.SetDestination(Player.Instance.transform.position);
    }

    private void Update()
    {
        if(Player.Instance == null)
        {
            _currentUnitState = UnitState.None;
        }

        switch (_currentUnitState)
        {
            case UnitState.None:
                break;

            case UnitState.Wait:

                Quaternion direction = Quaternion.LookRotation(Player.Instance.transform.position - transform.position, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, direction, _lookSpeed * Time.deltaTime);

                if ((Player.Instance.transform.position - transform.position).magnitude < _navMeshAgent.stoppingDistance)
                {
                    _animator.SetTrigger("Throw");
                    _currentUnitState = UnitState.None;
                }

                break;

            case UnitState.Move:

                if ((Player.Instance.transform.position - transform.position).magnitude < _navMeshAgent.stoppingDistance)
                {
                    _currentUnitState = UnitState.Wait;
                }

                break;
        }
    }

    private void LateUpdate()
    {
        _animator.SetFloat("Speed", _navMeshAgent.velocity.magnitude);
    }

    private void OnThrow()
    {
        if (Player.Instance.TryGetComponent(out IDamageTaker damageTaker))
        {
            damageTaker.TakeDamage(int.MaxValue);
        }
    }

    private void DasableRagdoll()
    {
        foreach (var item in GetComponentsInChildren<Transform>())
        {
            if (item.tag == "Ragdoll")
            {
                //item.GetComponent<Collider>().enabled = false;
                item.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    private void EnableRagdoll()
    {
        foreach (var item in GetComponentsInChildren<Transform>())
        {
            if (item.tag == "Ragdoll")
            {
                item.GetComponent<Rigidbody>().isKinematic = false;

                if (item.TryGetComponent(out PushMarker pushMarker))
                {
                    //pushMarker.OnPush();
                }
            }
        }
    }

}
