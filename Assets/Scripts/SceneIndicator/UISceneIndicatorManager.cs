using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneIndicatorManager : MonoBehaviour
{
    private static UISceneIndicatorManager _instance;
    public static UISceneIndicatorManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UISceneIndicatorManager>();
            }

            return _instance;
        }
    }

    [SerializeField] private UISceneIndicator sceneIndicatorPrefab;

    public void AddTarget(SceneIndicatorTarget sceneIndicatorTarget)
    {
        PoolComponent component = Pool.GetObject("SceneIndicator");
        UISceneIndicator sceneIndicator = component.GetComponent<UISceneIndicator>();
        sceneIndicator.transform.SetParent(transform);
        sceneIndicator.SetTarget(sceneIndicatorTarget.gameObject);
    }

}
