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
                //if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Floor"))
                //{
                //    actionOnHitFloor?.Invoke(hit.point);
                //}

                //if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Unit"))
                //{
                //    actionOnHitFloor?.Invoke(hit.point);
                //}

                actionTupHit?.Invoke(hit.point);
                //Debug.DrawLine(Player.Instance.transform.position + Vector3.up, hit.point, Color.red, 0.5f);

            }

        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;

    //    Vector3 startPosition = _tupPosition;
    //    startPosition.z = Camera.main.nearClipPlane;

    //    Vector3 endPosition = _tupPosition + _swipeDirection;
    //    endPosition.z = Camera.main.nearClipPlane;

    //    Gizmos.DrawLine(Camera.main.ScreenToWorldPoint(startPosition), Camera.main.ScreenToWorldPoint(endPosition));

    //}

}
