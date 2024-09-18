using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Unity.VisualScripting;

public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update

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

    float cooldown = 3f;
    float lastAttackedAt = -9999f;
    public GameObject bulletPref;

    int gunType = 0;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }



    //получает направление движенипя персонажа
    void HandleMovementInput()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

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
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        if (moveX != 0 || moveY != 0)
        {
            Animate(moveX, moveY, 1);
            moveXidle = moveX;
            moveYidle = moveY;
        }
        else
        {
            Animate(moveXidle, moveYidle, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        else if (!Input.GetMouseButtonUp(0)) 
        {
            animator.SetBool("IsAttack", false);
        }

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

    void Shoot()
    {
        animator.SetFloat("ShootX", GetShootingDirection().x);
        animator.SetFloat("ShootY", GetShootingDirection().y);

        animator.SetBool("IsAttack", true);

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

}
