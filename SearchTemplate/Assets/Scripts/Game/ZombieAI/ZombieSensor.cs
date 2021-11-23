using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSensor : MonoBehaviour
{
    [SerializeField] private float runawayDistance = 10.0f;
    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject diedView;
    [SerializeField] private float distanceToPlayer;


    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < runawayDistance)
        {
            animator.SetBool("isRunningAway", true);
        }
        else
        {
            animator.SetBool("isRunningAway", false);
        }

        if (diedView.activeInHierarchy)
        {
            animator.SetBool("isDead", true);
        }
}

    public float DistanceToPlayer()
    {
        return distanceToPlayer;
    }
}
