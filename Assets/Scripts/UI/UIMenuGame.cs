using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMenuGame : MonoBehaviour
{
    private static UIMenuGame _instance;
    public static UIMenuGame Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIMenuGame>();
            }

            return _instance;
        }
    }

    [SerializeField] private TextMeshProUGUI _messageText;

    public void ShowMessage(string message)
    {
        _messageText.text = message;
        _messageText.gameObject.SetActive(true);
    }

    public void HideMessage()
    {
        _messageText.gameObject.SetActive(false);
    }

}
