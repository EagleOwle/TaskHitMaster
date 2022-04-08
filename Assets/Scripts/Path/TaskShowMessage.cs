using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskShowMessage : WeypointTask
{
    public override void ExecAction(Action actionTaskOver)
    {
        _actionTaskOver = actionTaskOver;

        UIMenuGame.Instance.ShowMessage("Tup On Enemy");
        InputHandler.Instance.actionTupScreen += TaskOver;
    }

    public override void TaskOver()
    {
        UIMenuGame.Instance.HideMessage();
        InputHandler.Instance.actionTupScreen -= TaskOver;
        //_actionTaskOver?.Invoke();
    }
}
