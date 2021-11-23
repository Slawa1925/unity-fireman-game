using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public PlayerInput PlayerInput;

    [SerializeField] private GameObject player;
    public Rigidbody Rigidbody;
    public float Speed = 5f;
    private Vector3 _moveDirection;
    private Quaternion _viewDirection;
    private Vector3 moveTarget;
    private float wanderTimer;

    public float AttackTime = 1f;
    private float _attackTimer;
    public bool CanAttack => _attackTimer <= 0f;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void Move()
    {
        //var (moveDirection, viewDirection, shoot) = PlayerInput.CurrentInput();

        Rigidbody.velocity = _moveDirection.normalized * Speed;
        transform.rotation = _viewDirection;
    }

    public void RunAway()
    {
        _moveDirection = transform.position - player.transform.position;
        Move();
    }

    public void Wander()
    {
        wanderTimer -= Time.deltaTime;
        Move();

        if (wanderTimer <= 0f)
        {
            _moveDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
            wanderTimer = Random.Range(0f, 10f);
        }
    }

    public void ProcessAttack()
    {
        _attackTimer -= Time.deltaTime;

        if (CanAttack)
        {
            Attack();
        }
    }

    private void Attack()
    {
        _attackTimer = AttackTime;
    }
}
