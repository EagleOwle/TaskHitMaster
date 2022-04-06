using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _followSpeed;
    [SerializeField] Vector3 _followPositionOffSet;
    [SerializeField] Vector3 _lookPositionOffSet;

    private void LateUpdate()
    {
        if (_target == null) return;

        Vector3 targetPosition = _target.TransformPoint(_followPositionOffSet);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _followSpeed * Time.deltaTime);

        targetPosition = _target.TransformPoint(_lookPositionOffSet);
        transform.LookAt(targetPosition);
    }

}
