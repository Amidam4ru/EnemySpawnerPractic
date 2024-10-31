using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private Target _target;
    private float _degreeRotationInterpolation;

    public event Action<Enemy> Finished;

    private void Awake()
    {
        _degreeRotationInterpolation = 1f;
    }

    private void Update()
    {
        Vector3 moveDirection = (_target.gameObject.transform.position - transform.position).normalized;
        transform.position += moveDirection * _speed * Time.deltaTime;

        Vector3 directionToTarget = _target.transform.position - transform.position;
        directionToTarget.y = 0;

        if (directionToTarget != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, _degreeRotationInterpolation);
        }
    }

    public void SetTarget(Target target)
    {
        _target = target;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<Target>(out Target collisionTarget) && collisionTarget == _target)
        {
            Finished?.Invoke(this);
        }
    }
}
