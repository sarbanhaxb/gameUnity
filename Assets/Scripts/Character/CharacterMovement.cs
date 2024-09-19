using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Unity.VisualScripting;

public class CharacterMovement : MonoBehaviour
{
    // НУЖНО РАЗОБРАТЬСЯ С ВЫЛЕТОМ ДВУХ ПУЛЬ ИЗ-ЗА ВКЛЮЧЕНИЯ ОДНОВРЕМЕННО ДВУХ АНИМАЦИЙ

    [SerializeField]
    // скорость движения персонажа
    float moveSpeed = 5f;

    CharacterController controller;
    Vector2 moveDirection;

    float moveX;
    float moveY;

    float moveXidle;
    float moveYidle;

    Animator animator;

    Vector3 mouseDirection;

    public GameObject bulletPref;
    GunType gunType = GunType.Pistol;

    [Header("Melee Attack")]
    [SerializeField] Transform meleeAttackPoint;
    [SerializeField] float meleeAttackRange = 1f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }



    //получает направление движенипя персонажа

    void HandleAnimation()
    {
        if (moveDirection != Vector2.zero)
        {
            Animate(moveX, moveY, 1);
            moveXidle = moveX;
            moveYidle = moveY;
        }
        else
        {
            Animate(moveXidle, moveYidle, 0);
        } 
    }



    // Update is called once per frame
    void Update()
    {
        HandleAttack();
        HandleMovementInput();
        HandleAnimation();
        HandleChangeGun();
    }

    void HandleMovementInput()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }


    private void FixedUpdate()
    {
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    void Animate(float moveX, float moveY, int layer)
    {
        switch (layer)
        {
            case 0:
                animator.SetLayerWeight(0, 1);
                animator.SetLayerWeight(1, 0);
                break;
            case 1:
                animator.SetLayerWeight(1, 1);
                animator.SetLayerWeight(0, 0);
                break;
        }

        animator.SetFloat("MoveX", moveX);
        animator.SetFloat("MoveY", moveY);
    }


    void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0) && moveDirection == Vector2.zero && gunType==GunType.Pistol)
        {
            animator.SetFloat("ShootX", GetShootingDirection().x);
            animator.SetFloat("ShootY", GetShootingDirection().y);
            animator.SetBool("IsAttackIdle", true);
        }
        else if (Input.GetMouseButtonDown(0) && moveDirection != Vector2.zero && gunType == GunType.Pistol)
        {
            animator.SetFloat("ShootX", GetShootingDirection().x);
            animator.SetFloat("ShootY", GetShootingDirection().y);
            animator.SetBool("IsAttackMove", true);
        }

        else if (Input.GetMouseButtonDown(0) && gunType == GunType.Knife)
        {
            animator.SetFloat("ShootX", GetShootingDirection().x);
            animator.SetFloat("ShootY", GetShootingDirection().y);
            animator.SetTrigger("Melee");
        }

        else if (!Input.GetMouseButtonUp(0))
        {
            animator.SetBool("IsAttackMove", false);
            animator.SetBool("IsAttackIdle", false);
        }
    }

    void ShootIdle()
    {
        animator.SetFloat("ShootX", GetShootingDirection().x);
        animator.SetFloat("ShootY", GetShootingDirection().y);
        animator.SetBool("IsAttackIdle", true);
    }
    void ShootMove()
    {
        animator.SetFloat("ShootX", GetShootingDirection().x);
        animator.SetFloat("ShootY", GetShootingDirection().y);
        animator.SetBool("IsAttackMove", true);
    }

    //запускает анимацию стрельбы
    public void Bullet()
    {
        Vector3 shootingDirection = GetShootingDirection();
        float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
        GameObject bulletInstance = Instantiate(bulletPref, transform.position + shootingDirection, Quaternion.identity);

        bulletInstance.GetComponent<Rigidbody2D>().velocity = shootingDirection * 5f;

        bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Destroy(bulletInstance, 4f);
    }

    //возвращает позицию клика курсора
    Vector3 GetShootingDirection()
    {
        mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseDirection.z = 0f;
        return (mouseDirection - transform.position).normalized;
    }

    //изменить оружие
    void HandleChangeGun()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gunType = GunType.Pistol;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gunType = GunType.Knife;
        }

        Debug.Log(gunType);
    }

    void MeleeAttack()
    {
        animator.SetTrigger("Melee");
        Collider2D enemy = Physics2D.OverlapCircle(meleeAttackPoint.position, meleeAttackRange);
        if (enemy != null)
        {
            Debug.Log(enemy.name);
        }
        else
        {
            Debug.Log("BOBR KURVA");
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(meleeAttackPoint.position, meleeAttackRange);
    }

}
