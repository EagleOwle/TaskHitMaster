using System;
using UnityEngine;

public abstract class WeypointTask : MonoBehaviour
{
    protected Action _actionTaskOver;
    public abstract void ExecAction(Action actionTaskOver);
    public abstract void TaskOver();
}
