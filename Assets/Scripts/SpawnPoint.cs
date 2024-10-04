using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Vector3 _turningEnemy;

    public Quaternion TurningEnemy => Quaternion.Euler(_turningEnemy);
}
