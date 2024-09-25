using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public GunType gunType = GunType.Pistol;

    CharacterMovement move;

    [Header("Melee Attack")]
    [SerializeField] Transform meleeAttackPoint;
    [SerializeField] GameObject bulletPref;
    [SerializeField] float meleeAttackRange = 1f;
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleChangeGun();
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

    //включает ближний бой
    public void MeleeAttack()
    {
        //animator.SetTrigger("Melee");
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

    //отрисовывает радиус удара
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(meleeAttackPoint.position, meleeAttackRange);
    }

    public void Bullet()
    {
        Instantiate(bulletPref, transform.position + move.GetShootingDirection(), Quaternion.identity);
    }
}
