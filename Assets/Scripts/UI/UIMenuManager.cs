using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIMenuManager : MonoBehaviour
{
    private static UIMenuManager _instance;
    public static UIMenuManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIMenuManager>();
            }

            return _instance;
        }
    }

    private bool _inMenu = false;
    public bool InMenu => _inMenu;
    public Action<string> actionChangeMenu;

    [SerializeField] private UIMenu _startMenu;
    [SerializeField] private UIMenu _winMenu, _loosMenu;

    public void ShowStartMenu()
    {
        _startMenu.gameObject.SetActive(true);
        actionChangeMenu?.Invoke(_startMenu.nameMenu);
        _inMenu = true;
    }

    public void ShowWinMenu()
    {
        _winMenu.gameObject.SetActive(true);
        actionChangeMenu?.Invoke(_winMenu.nameMenu);
        _inMenu = true;
    }

    public void ShowLoosMenu()
    {
        _loosMenu.gameObject.SetActive(true);
        actionChangeMenu?.Invoke(_loosMenu.nameMenu);
        _inMenu = true;
    }

    public void ShowStartMenu( out string nameMenu)
    {
        nameMenu = _startMenu.nameMenu;
        _startMenu.gameObject.SetActive(true);
        actionChangeMenu?.Invoke(_startMenu.nameMenu);
        _inMenu = true;
    }

    public void HideStartMenu()
    {
        _startMenu.gameObject.SetActive(false);
        actionChangeMenu?.Invoke(_startMenu.nameMenu);
        _inMenu = false;
    }

}
