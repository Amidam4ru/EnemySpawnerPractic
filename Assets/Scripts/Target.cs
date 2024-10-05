using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Transform _firstPoutePoint;
    [SerializeField] private Transform _secondPoutePoint;
    [SerializeField] private float _acceptablePproximity;
    [SerializeField] private float _speed;

    private Vector3 _target;

    private void Start()
    {
        _target = _firstPoutePoint.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _target) <= _acceptablePproximity)
        {
            _target = _target == _firstPoutePoint.position ? _secondPoutePoint.position : _firstPoutePoint.position;
        }
    }
}
