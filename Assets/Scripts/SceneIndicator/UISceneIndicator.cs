using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneIndicator : MonoBehaviour
{
    [Header("�������� ������")]
    [Tooltip("������������ ��� ������ ������ �� �����. ������ � ������ ������ ������ ��������� ��������� Camera")]
    [SerializeField] protected string cameraName = "Main Camera";

    [Header("������ �� ������� ������� - ����")]
    [SerializeField] protected Vector3 offset;

    [Header("������������ ���� ��������� ����������")]
    [Tooltip("���� ������������� �����: ������ - ������ � ������ - ������ ������")]
    [SerializeField] protected float maxAngle;

    [Header("������ ��������, ������� ���/����")]
    [SerializeField] protected GameObject[] indicatorArrayObj;

    private GameObject target;
    public GameObject Target => target;

    private bool visible = false;
    protected bool Visible
    {
        get
        {
            return visible;
        }
        set
        {
            visible = value;

            foreach (GameObject obj in indicatorArrayObj)
            {
                obj.SetActive(value);
            }

        }
    }

    protected Camera currentCamera;
    protected RectTransform rectTarnsform;

    protected virtual void Awake()
    {
        rectTarnsform = GetComponent<RectTransform>();
    }

    protected virtual void OnEnable()
    {
        foreach (GameObject item in indicatorArrayObj)
        {
            item.SetActive(false);
        }
    }

    protected virtual void LateUpdate()
    {
        Visible = true;

        #region ���������, ���� ��� �������-����
        if (target == null)
        {
            Visible = false;
            Destroy(gameObject);
            return;
        }
        #endregion

        #region ���� ������ �� �����
        if (currentCamera == null)
        {
            if (GameObject.Find(cameraName) != null)
            {
                GameObject tmp = GameObject.Find(cameraName);
                currentCamera = tmp.GetComponent<Camera>();
            }
            else
            {
                Visible = false;
                return;
            }
        }
        #endregion

        #region ��������� ����
        if (CheckAngle() == false)
        {
            Visible = false;
            return;
        }
        #endregion

        #region ������ ������� �� ������
        rectTarnsform.position = currentCamera.WorldToScreenPoint(target.transform.position + offset);
        #endregion

    }

    protected virtual bool CheckAngle()
    {
        if (currentCamera == null)
        {
            return false;
        }

        if (target == null)
        {
            return false;
        }

        Vector3 to = currentCamera.transform.forward;
        Vector3 from = target.transform.position - currentCamera.transform.position;

        if (Vector3.Angle(from, to) > maxAngle)
        {
            return false;
        }

        return true;
    }

    public void SetTarget(GameObject obj)
    {
        target = obj;

        foreach (GameObject item in indicatorArrayObj)
        {
            item.SetActive(true);
        }

    }


}
