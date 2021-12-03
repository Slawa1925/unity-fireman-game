using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int MaxHealth;
    [SerializeField] private UnityEvent _event;
    [SerializeField] int _health;
    public int HealthPoints => _health;

    public void Start()
    {
        _health = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_event != null)
            _event.Invoke();
    }
}
