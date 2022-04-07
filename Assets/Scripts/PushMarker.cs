using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushMarker : MonoBehaviour
{
    [SerializeField] private float _pushPower = 1000;

    private Rigidbody _rigidbody;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Invoke(nameof(OnPush), Time.deltaTime);
    }

    public void OnPush()
    {
        Vector3 pushDirection = -(Player.Instance.transform.position - transform.position).normalized;
        _rigidbody.AddForce(pushDirection * _pushPower);
    }
}
