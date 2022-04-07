using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public WayPoint[] PathPoints => _pathPoints;
    [SerializeField] private WayPoint[] _pathPoints;

    public int GetNextPointIndex(int currentIndex, bool loop = false)
    {
        if (currentIndex + 1 < _pathPoints.Length)
        {
            currentIndex += 1;
        }
        else
        {
            if (loop)
            {
                currentIndex = 0;
            }
        }

        return currentIndex;
    }

}
