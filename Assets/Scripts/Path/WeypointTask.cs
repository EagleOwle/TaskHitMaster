using System;
using UnityEngine;

public abstract class WeypointTask : MonoBehaviour
{
    public abstract void ExecAction(Action actionTaskOver);
    public abstract void TaskOver();
}
