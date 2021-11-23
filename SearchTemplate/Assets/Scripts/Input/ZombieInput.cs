using System.Linq;
using Game;
using UnityEngine;


public class ZombieInput : PlayerInput
{
    //[SerializeField] private ZombieMap _zombieMap;
    //[SerializeField] private Transform _player;
    //[SerializeField] private float _fireDistance;
    private bool _isFiring;
    private Vector3 _direction;


    public override (Vector3 moveDirection, Quaternion viewDirection, bool shoot) CurrentInput()
    {
        return (_direction, Quaternion.LookRotation(_direction), _isFiring);
    }

    public void MoveTo(Vector3 target)
    {
        _direction = (target - transform.position);
    }
}
