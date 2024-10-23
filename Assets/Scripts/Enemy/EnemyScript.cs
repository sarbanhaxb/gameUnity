using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class EnemyScript : MonoBehaviour
{

    public int EnemyMaxHP { get; set; } = 100;
    public int EnemyHP { get; set; }
    public int EnemyDamage { get; set; }
    public int EnemySpeed { get; set; }

    float roamRadius = 10f;

    float distanceToPlayer;
    GameObject player;

    bool attacking;

    float chaseDistance = 5f;
    float attackDistance = 0.6f;
    float roamingDistance = 10f;
    ZombieState currentState = ZombieState.Roaming;
    CharacterStats characterStats;


    float oldX;
    float oldY;
    float DifX = 0;
    float DifY = 0;


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
        EnemyDamage = 1;
        EnemySpeed = 1;
        player = GameObject.FindGameObjectWithTag("Player");
        roamPoint = transform.position;
        animator = GetComponent<Animator>();

        characterStats = player.GetComponent<CharacterStats>();
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
        animator.SetBool("IsAttack", false);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, EnemySpeed * Time.deltaTime);
    }

    private void ZombieAttacking()
    {
        animator.SetBool("IsAttack", true);
    }

    void AnimatorSetData()
    {
        if (currentState == ZombieState.Attacking)
        {

            animator.SetFloat("eX", getN(DifX));
            animator.SetFloat("eY", getN(DifY));
        }
        else
        {
            DifX = transform.position.x - oldX;
            DifY = transform.position.y - oldY;

            animator.SetFloat("eX", DifX);
            animator.SetFloat("eY", DifY);

            oldX = transform.position.x;
            oldY = transform.position.y;
        }
    }

    int getN(double n)
    {
        if (n > 0) return 1;
        else if (n < 0) return -1;
        else return 0;
    }


    public void Attack()
    {
        if (distanceToPlayer < attackDistance)
            characterStats.HP -= EnemyDamage;
        Debug.Log(characterStats.HP);

        if (characterStats.HP <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    Vector2 GetRandomPoint() => (Vector2)transform.position + Random.insideUnitCircle * roamRadius;

}
