using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenuEndLevel : UIMenu
{
    [SerializeField] private Button _exitButton;

    private void Awake()
    {
        _exitButton.onClick.AddListener(OnButtonExit);
    }

    public void OnButtonExit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
