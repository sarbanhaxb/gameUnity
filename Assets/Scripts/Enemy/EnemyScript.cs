using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyScript : MonoBehaviour
{

    public int EnemyMaxHP { get; set; }
    public int EnemyHP { get; set; }
    public int EnemyDamage { get; set; }
    public int EnemySpeed { get; set; }
    float roamRadius = 10f;

    float distanceToPlayer;
    GameObject player;

    float chaseDistance = 5f;
    float attackDistance = 0.7f;
    float roamingDistance = 10f;
    ZombieState currentState = ZombieState.Roaming;


    float oldX;
    float oldY;


    Vector2 roamPoint;

    Vector2 vector;

    Animator animator;

    public enum ZombieState
    {
        Roaming,
        Chasing,
        Attacking
    }

    void Start()
    {
        EnemyHP = EnemyMaxHP;
        EnemyDamage = 25;
        EnemySpeed = 1;
        player = GameObject.FindGameObjectWithTag("Player");
        roamPoint = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        SwitchState(distanceToPlayer);

        AnimatorSetData();

        switch (currentState)
        {
            case ZombieState.Roaming:
                ZombieRoaming();
                break;
            case ZombieState.Chasing:
                ZombieChasing();
                break;
            case ZombieState.Attacking:
                ZombieAttacking();
                break;
        }
    }

    void SwitchState(float distanceToPlayer)
    {
        if (distanceToPlayer < attackDistance)
            currentState = ZombieState.Attacking;
        else if (distanceToPlayer < chaseDistance)
            currentState = ZombieState.Chasing;
        else if (distanceToPlayer < roamingDistance)
            currentState = ZombieState.Roaming;
    }


    private void ZombieRoaming()
    {
        transform.position = Vector3.MoveTowards(transform.position, roamPoint, EnemySpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, roamPoint) < 0.1f)
            roamPoint = GetRandomPoint();
    }

    private void ZombieChasing()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, EnemySpeed * Time.deltaTime);
    }

    private void ZombieAttacking()
    {
        Debug.Log("ATTACK!!1");
    }

    void AnimatorSetData()
    {

        animator.SetFloat("eX", transform.position.x - oldX);
        animator.SetFloat("eY", transform.position.y - oldY);

        oldX = transform.position.x;
        oldY = transform.position.y;
    }

    Vector2 GetRandomPoint() => (Vector2)transform.position + Random.insideUnitCircle * roamRadius;

}
