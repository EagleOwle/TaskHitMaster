using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private static Player _instance;
    public static Player Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Player>();
            }

            return _instance;
        }
    }

    [SerializeField] private float _lookSpeed = 10;
    [SerializeField] private float _attackCooldown = 3f;
    [SerializeField] private UnitState _currentUnitState;
    [SerializeField] private Transform _armTransform;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    private PathFallow _pathFallow;
    private Vector3 _lookTarget;
    private Projectile _projectile;
    private float _currentAttackCooldown;
    private Vector3 _shootTargetPosition;
    private List<Enemy> _enemys;
    private HealthHandler _healthHandler;
    private bool _isDead = false;
    public bool IsDead => _isDead;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _healthHandler = GetComponent<HealthHandler>();
    }

    private void OnEnable()
    {
        GetComponent<PathFallow>().actionChangeFollowState += ChangeFollowState;
        InputHandler.Instance.actionTupHit += OnTupHit;
        _currentAttackCooldown = Time.time + _attackCooldown;
        _healthHandler.ActionHealthChanged += OnHealthChanged;
        _isDead = false;
    }

    private void ChangeFollowState(UnitState state)
    {
        switch (state)
        {
            case UnitState.Wait:
                break;
            case UnitState.Move:
                break;
        }

        _currentUnitState = state;
    }

    public void SetNextTarget(List<Enemy> enemys)
    {
        _enemys = enemys;
        SetLookTarget();
    }

    private void SetLookTarget()
    {
        if (_enemys.Count == 0) return;
        _enemys = _enemys.OrderBy((d) => (d.transform.position - transform.position).sqrMagnitude).ToList();
        _lookTarget = _enemys[0].transform.position;

    }

    private void OnTupHit(Vector3 worldPosition)
    {
        if (_currentAttackCooldown > Time.time) return;

        if (_currentUnitState != UnitState.Wait) return;

        if (UIMenuManager.Instance.InMenu) return;

        _shootTargetPosition = worldPosition;
        _animator.SetTrigger("Throw");
        _currentAttackCooldown = Time.time + _attackCooldown;
        _navMeshAgent.isStopped = true;
    }

    private void FixedUpdate()
    {
        if (_currentUnitState == UnitState.Wait)
        {
            Quaternion direction = Quaternion.LookRotation(_lookTarget - transform.position, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, direction, _lookSpeed * Time.deltaTime);
        }
    }

    private void Update()
    {
        _animator.SetFloat("Speed", _navMeshAgent.velocity.magnitude);
        ReloadWeapon();
    }

    private void ReloadWeapon()
    {
        if (_projectile != null) return;

        if (_currentAttackCooldown <= Time.time)
        {
            PoolComponent component = Pool.GetObject("Projectile");
            _projectile = component.GetComponent<Projectile>();
            _projectile.transform.SetParent(_armTransform);
            _projectile.transform.localPosition = Vector3.zero;
            _projectile.transform.localRotation = Quaternion.identity;
        }
    }

    private void OnDisable()
    {
        GetComponent<PathFallow>().actionChangeFollowState -= ChangeFollowState;

        if (InputHandler.Instance != null)
        {
            InputHandler.Instance.actionTupHit -= OnTupHit;
        }
    }

    private void OnThrow()
    {
        _projectile.Launch(_shootTargetPosition);
        _projectile = null;
    }

    private void ReadyForWalk()
    {
        _navMeshAgent.isStopped = false;
    }

    private void OnHealthChanged(float e)
    {
        if (e <= 0)
        {
            _animator.SetTrigger("Dead");
            UIMenuManager.Instance.ShowLoosMenu();
            _isDead = true;
        }
        else
        {
            _animator.SetTrigger("Hit");
        }
    }

}
