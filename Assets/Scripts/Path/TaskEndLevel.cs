using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskEndLevel : WeypointTask
{
    public override void ExecAction(Action actionTaskOver)
    {
        UIMenuManager.Instance.ShowWinMenu();
    }

    public override void TaskOver()
    {
        
    }
}
