using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    CharacterMovement move;
    Vector3 shootingDirection;
    void Start()
    {
        move = FindObjectOfType<CharacterMovement>();

        shootingDirection = move.GetShootingDirection();
        float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        gameObject.GetComponent<Rigidbody2D>().velocity = shootingDirection * 5f;

        Destroy(gameObject, 4f);
    }
}