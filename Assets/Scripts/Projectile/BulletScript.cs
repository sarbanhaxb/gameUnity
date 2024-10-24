using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    CharacterMovement move;
    Vector3 shootingDirection;
    CharacterAttack attack;
    void Start()
    {
        move = FindObjectOfType<CharacterMovement>();
        attack = FindObjectOfType<CharacterAttack>();

        shootingDirection = move.GetShootingDirection();
        float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        gameObject.GetComponent<Rigidbody2D>().velocity = shootingDirection * attack.HandleChangeGun().AttackSpeed;

        Destroy(gameObject, 4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var eminem = collision.gameObject.GetComponent<EnemyScript>();
        if (collision.tag == "Enemy" && eminem.EnemyHP > 0)
        {
            eminem.EnemyHP -= 10;
            if (eminem.EnemyHP <= 0)
            {
                var anim = collision.gameObject.GetComponent<Animator>();
                anim.SetTrigger("DeathTrigger");

                eminem.EnemySpeed = 0;
                Destroy(collision.gameObject, 20);
                eminem.DropGoods();
                eminem.currentState = EnemyScript.ZombieState.Death;
                eminem.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            }
            Destroy(gameObject);
        }
    }
}
