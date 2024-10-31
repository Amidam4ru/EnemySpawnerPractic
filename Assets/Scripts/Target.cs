using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Transform[] _routePoints;
    [SerializeField] private Transform _secondPoutePoint;
    [SerializeField] private float _acceptablePproximity;
    [SerializeField] private float _speed;

    private Vector3 _target;
    private int _targetCount;

    private void Start()
    {
        _targetCount = 0;
        _target = _routePoints[_targetCount].position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _target) <= _acceptablePproximity)
        {
            _targetCount++;

            if (_targetCount == _routePoints.Length)
            {
                _targetCount = 0;
            }

            _target = _routePoints[_targetCount].position;
        }
    }
}
