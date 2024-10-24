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
    public ZombieState currentState = ZombieState.Roaming;
    CharacterStats characterStats;


    float oldX;
    float oldY;
    float DifX = 0;
    float DifY = 0;

    public List<GameObject> items;

    Vector2 roamPoint;

    Vector2 vector;

    Animator animator;

    public enum ZombieState
    {
        Roaming,
        Chasing,
        Attacking,
        Death
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
            float oX = DifX;
            float oY = DifY;

            if (oX < 0 && oY < 0)
            {
                if (Math.Abs(oX) > Mathf.Abs(oY))
                {
                    oX = -1; oY = 0;
                }
                else
                {
                    oX = 0; oY = -1;
                }
            }
            else if (oX > 0 && oY < 0)
            {
                if (Math.Abs(oX) < Mathf.Abs(oY))
                {
                    oX = 0; oY = -1;
                }
                else
                {
                    oX = 1; oY = 0;
                }
            }
            else if (oX > 0 && oY > 0)
            {
                if (Math.Abs(oX) > Math.Abs(oY))
                {
                    oX = 1; oY = 0;
                }
                else
                {
                    oX = 0; oY = 1;
                }
            }
            else if (oX < 0 && oY > 0)
            {
                if (Math.Abs(oX) > Math.Abs(oY))
                {
                    oX = -1; oY = 0;
                }
                else
                {
                    oX = 0; oY = 1;
                }
            }
            animator.SetFloat("eX", oX);
            animator.SetFloat("eY", oY);
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

    public void Attack()
    {
        if (distanceToPlayer < attackDistance)
            if (characterStats.Armor >= EnemyDamage)
                characterStats.Armor -= EnemyDamage;
            else
            {
                characterStats.HP -= Math.Abs(characterStats.Armor - EnemyDamage);
                characterStats.Armor = 0;
            }
        Debug.Log(characterStats.HP);

        if (characterStats.HP <= 0)
            SceneManager.LoadScene(0);
    }

    public void DropGoods()
    {
        var randomCount = Random.Range(1, 4);

        for (int i = 0; i < randomCount; i++)
        {
            var randomItem = Random.Range(1, items.Count);
            Instantiate(items[randomItem], GetRandomPosInRadius(transform.position, 1), Quaternion.identity);
        }
    }
    Vector2 GetRandomPoint() => (Vector2)transform.position + Random.insideUnitCircle * roamRadius;
    Vector3 GetRandomPosInRadius(Vector3 centerPoint, float radius) => centerPoint + (Random.insideUnitSphere * radius);
}
