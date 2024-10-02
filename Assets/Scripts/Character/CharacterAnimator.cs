using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterAnimator : MonoBehaviour
{
    Animator animator;
    CharacterMovement move;
    CharacterAttack attack;


    float moveXidle;
    float moveYidle;

    void Start()
    {
        animator = GetComponent<Animator>();
        move = GetComponent<CharacterMovement>();
        attack = GetComponent<CharacterAttack>();
    }

    void Update()
    {
        HandleAnimation();
        HandleAttack();
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

    void HandleAnimation()
    {
        if (move.moveDirection != Vector2.zero)
        {
            Animate(move.moveX, move.moveY, 1);
            moveXidle = move.moveX;
            moveYidle = move.moveY;
        }
        else
        {
            Animate(moveXidle, moveYidle, 0);
        }
    }

    void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0) && move.moveDirection == Vector2.zero && attack.gunType == GunType.Pistol && attack.HandleChangeGun().Ammo > 0)
        {
            animator.SetFloat("ShootX", move.GetShootingDirection().x);
            animator.SetFloat("ShootY", move.GetShootingDirection().y);
            animator.SetBool("IsAttackIdle", true);
        }
        else if (Input.GetMouseButtonDown(0) && move.moveDirection != Vector2.zero && attack.gunType == GunType.Pistol && attack.HandleChangeGun().Ammo > 0)
        {
            animator.SetFloat("ShootX", move.GetShootingDirection().x);
            animator.SetFloat("ShootY", move.GetShootingDirection().y);
            animator.SetBool("IsAttackMove", true);
        }

        else if (Input.GetMouseButtonDown(0) && move.moveDirection == Vector2.zero && attack.gunType == GunType.Crossbow && attack.HandleChangeGun().Ammo > 0)
        {
            animator.SetFloat("ShootX", move.GetShootingDirection().x);
            animator.SetFloat("ShootY", move.GetShootingDirection().y);
            animator.SetBool("IsCrossbowIdle", true);
        }
        else if (Input.GetMouseButtonDown(0) && move.moveDirection != Vector2.zero && attack.gunType == GunType.Crossbow && attack.HandleChangeGun().Ammo > 0)
        {
            animator.SetFloat("ShootX", move.GetShootingDirection().x);
            animator.SetFloat("ShootY", move.GetShootingDirection().y);
            animator.SetBool("IsCrossbowMove", true);
        }


        else if (Input.GetMouseButtonDown(0) && attack.gunType == GunType.Knife)
        {
            animator.SetFloat("ShootX", move.GetShootingDirection().x);
            animator.SetFloat("ShootY", move.GetShootingDirection().y);
            animator.SetTrigger("Melee");
            attack.MeleeAttack();
        }

        else if (!Input.GetMouseButtonUp(0))
        {
            animator.SetBool("IsAttackMove", false);
            animator.SetBool("IsAttackIdle", false);
            animator.SetBool("IsCrossbowIdle", false);
            animator.SetBool("IsCrossbowMove", false);
        }
    }
}
