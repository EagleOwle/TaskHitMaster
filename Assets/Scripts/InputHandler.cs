using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    private static InputHandler _instance;
    public static InputHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<InputHandler>();
            }

            return _instance;
        }
    }

    public Action<Vector3> actionTupHit;

    private Vector3 _tupWorldPosition;
    private RaycastHit hit;
    private Ray ray;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                actionTupHit?.Invoke(hit.point);
            }

        }
    }

}
