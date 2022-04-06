using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFallow : MonoBehaviour
{
    public Action<UnitState> actionChangeFollowState;
    [SerializeField] private bool loopPath;

    private NavMeshAgent _navMeshAgent;
    private UnitState _followState;
    private UnitState SetFollowState
    {
        set
        {
            _followState = value;

            switch (_followState)
            {
                case UnitState.Wait:
                    _navMeshAgent.updatePosition = false;
                    _navMeshAgent.updateRotation = false;
                    break;
                case UnitState.Move:
                    _navMeshAgent.updatePosition = true;
                    _navMeshAgent.updateRotation = true;
                    break;
            }


            actionChangeFollowState?.Invoke(_followState);
        }
    }
    private WayPoint[] pathPoint;

    private int currentPointIndex;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        pathPoint = GameObject.FindObjectsOfType<WayPoint>();
        currentPointIndex = -1;
        Invoke(nameof(GoNext), 5);
    }

    private void Update()
    {
        if (_followState == UnitState.Move)
        {
            if ((pathPoint[currentPointIndex].transform.position - transform.position).magnitude < _navMeshAgent.stoppingDistance)
            {
                SetFollowState = UnitState.Wait;
                pathPoint[currentPointIndex].PlayerOnPoint();
                pathPoint[currentPointIndex].actionPlayerEndPoint += OnEventPlayerEndPoint;
            }
        }
    }

    private void OnEventPlayerEndPoint()
    {
        Invoke(nameof(GoNext), 5);
    }

    private void GoNext()
    {
        currentPointIndex = GetNextPointIndex(currentPointIndex, loopPath);
        _navMeshAgent.SetDestination(pathPoint[currentPointIndex].transform.position);
        SetFollowState = UnitState.Move;
    }

    private int GetNextPointIndex(int currentIndex, bool loop = false)
    {
        if (currentIndex + 1 < pathPoint.Length)
        {
            currentIndex += 1;
        }
        else
        {
            if(loop)
            {
                currentIndex = 0;
            }
        }

        return currentIndex;
    }

}
