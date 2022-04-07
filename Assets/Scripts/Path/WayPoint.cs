using System;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public WeypointTask[] weypointTask;

    public Action actionPlayerEndPoint;

    public void PlayerOnPoint()
    {
        foreach (var item in weypointTask)
        {
            item.ExecAction(actionPlayerEndPoint);
        }
        
    }

}
