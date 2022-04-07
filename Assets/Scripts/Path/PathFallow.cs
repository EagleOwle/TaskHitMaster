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
    [SerializeField] private Path mainPath;

    private int currentPointIndex;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        currentPointIndex = -1;
        Invoke(nameof(GoNext), 1);
    }

    private void Update()
    {
        if (_followState == UnitState.Move)
        {
            if ((mainPath.PathPoints[currentPointIndex].transform.position - transform.position).magnitude < _navMeshAgent.stoppingDistance)
            {
                SetFollowState = UnitState.Wait;
                mainPath.PathPoints[currentPointIndex].actionPlayerEndPoint += OnEventPlayerEndPoint;
                mainPath.PathPoints[currentPointIndex].PlayerOnPoint();
                
            }
        }
    }

    private void OnEventPlayerEndPoint()
    {
        //Debug.LogError("OnEventPlayerEndPoint");
        Invoke(nameof(GoNext), 1);
    }

    private void GoNext()
    {
        currentPointIndex = mainPath.GetNextPointIndex(currentPointIndex, loopPath);
        _navMeshAgent.SetDestination(mainPath.PathPoints[currentPointIndex].transform.position);
        SetFollowState = UnitState.Move;
    }

    

}
