using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] WayPoint _targetPoint;
    [SerializeField] private float _lookSpeed = 10;
    private NavMeshAgent _navMeshAgent;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Collider baseCollider;
    private Collider pelvicCollider;
    private HealthHandler _healthHandler;
    [SerializeField] private UnitState _currentUnitState;

    private Vector3 _lookTarget;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _healthHandler = GetComponent<HealthHandler>();
        baseCollider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        _targetPoint.actionPlayerOnPoint += PlayerOnPoint;
        _healthHandler.HealthChanged += OnHealthChanged;
        _currentUnitState = UnitState.Wait;
        DasableRagdoll();
    }

    private void OnHealthChanged(object sender, float e)
    {
        if (e <= 0)
        {
            //Debug.LogError("Health end");
            Destroy(_rigidbody);
            //baseCollider.enabled = false;
            //_navMeshAgent.enabled = false;
            //_animator.enabled = false;
            //_healthHandler.enabled = false;
            Destroy(baseCollider);
            Destroy(_navMeshAgent);
            Destroy(_animator);
            Destroy(_healthHandler);
            _targetPoint.DestroyEnemy(this);
            EnableRagdoll();
            Destroy(this);
        }
    }

    private void OnDisable()
    {
        _targetPoint.actionPlayerOnPoint -= PlayerOnPoint;
        GetComponent<HealthHandler>().HealthChanged -= OnHealthChanged;
    }

    private void PlayerOnPoint()
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

        if (_currentUnitState == UnitState.Move)
        {
            if ((Player.Instance.transform.position - transform.position).magnitude < _navMeshAgent.stoppingDistance)
            {
                _currentUnitState = UnitState.Wait;
            }
        }
    }

    private void FixedUpdate()
    {
        if (Player.Instance == null)
        {
            _currentUnitState = UnitState.None;
        }

        if (_currentUnitState == UnitState.Wait)
        {
            Quaternion direction = Quaternion.LookRotation(Player.Instance.transform.position - transform.position, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, direction, _lookSpeed * Time.deltaTime);

            if ((Player.Instance.transform.position - transform.position).magnitude < _navMeshAgent.stoppingDistance)
            {
                _animator.SetTrigger("Throw");
                _currentUnitState = UnitState.None;
            }
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
                item.GetComponent<Collider>().enabled = false;
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
                item.GetComponent<Collider>().enabled = true;
                item.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

}
