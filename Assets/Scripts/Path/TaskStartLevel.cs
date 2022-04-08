using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskStartLevel : WeypointTask
{
    private string nameMenu;

    public override void ExecAction(Action actionTaskOver)
    {
        _actionTaskOver = actionTaskOver;
        UIMenuManager.Instance.ShowStartMenu(out nameMenu);
        UIMenuManager.Instance.actionChangeMenu += OnCloseMenu;
        
    }

    private void OnCloseMenu(string value)
    {
        if (value == "")
        {
            Debug.LogError("Name menu not set");
            return;
        }

        if (value == nameMenu)
        {
            UIMenuManager.Instance.actionChangeMenu -= OnCloseMenu;
            TaskOver();
        }
    }

    public override void TaskOver()
    {
        _actionTaskOver?.Invoke();

    }
}
