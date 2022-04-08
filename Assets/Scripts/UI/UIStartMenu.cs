using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStartMenu : UIMenu
{
    [SerializeField] private Button _goButton;

    private void Awake()
    {
        _goButton.onClick.AddListener(OnButtonGo);
    }

    public void OnButtonGo()
    {
        UIMenuManager.Instance.HideStartMenu();
    }

}
