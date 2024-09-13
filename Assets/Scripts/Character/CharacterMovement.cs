using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    float moveSpeed = 5f;

    CharacterController controller;
    Vector2 moveDirection;

    float moveX;
    float moveY;

    float moveXidle;
    float moveYidle;

    Animator animator;
    EventSystem IPointerDownHandler = null;

    void Start()
    {

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

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


        if (Input.GetMouseButtonDown(0))
            Attack();
        else
            animator.SetBool("IsAttack", false);
    }

    void Attack()
    {
        animator.SetBool("IsAttack", true);
        animator.SetFloat("MoveX", moveXidle);
        animator.SetFloat("MoveY", moveYidle);
    }

}
