
using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskKiller : WeypointTask
{
    [SerializeField] private List<Enemy> enemys;

    private Action _actionTaskOver;

    public override void ExecAction(Action actionTaskOver)
    {
        _actionTaskOver = actionTaskOver;
        Player.Instance.SetNextTarget(enemys);

        foreach (var item in enemys)
        {
            item.StartHunt();
            item.actionIsDead += DestroyEnemy;
        }
    }

    public void DestroyEnemy(Enemy enemy)
    {
        //Debug.LogError("DestroyEnemy");
        enemy.actionIsDead -= DestroyEnemy;
        enemys.Remove(enemy);
        Player.Instance.SetNextTarget(enemys);
        if (enemys.Count == 0)
        {
            TaskOver();
        }
    }

    public override void TaskOver()
    {
        _actionTaskOver?.Invoke();

    }
}
