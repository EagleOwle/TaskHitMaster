using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneIndicatorTarget : MonoBehaviour
{
    private void Start()
    {
        UISceneIndicatorManager.Instance.AddTarget(this);
    }
}
