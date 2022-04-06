using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public Action actionPlayerOnPoint;
    public Action actionPlayerEndPoint;

    [SerializeField] private List<Enemy> enemys;

    public void PlayerOnPoint()
    {
        actionPlayerOnPoint?.Invoke();
        Player.Instance.SetNextTarget(enemys);
    }

    public void DestroyEnemy(Enemy enemy)
    {
        enemys.Remove(enemy);
        Player.Instance.SetNextTarget(enemys);
        if (enemys.Count == 0)
        {
            actionPlayerEndPoint?.Invoke();
        }
    }

}
