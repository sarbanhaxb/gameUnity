using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public GunType gunType = GunType.Pistol;

    CharacterMovement move;
    CharacterStats stats;
    WeaponScript Stats;
    public List<WeaponScript> weapons;


    [Header("Melee Attack")]
    [SerializeField] Transform meleeAttackPoint;
    [SerializeField] GameObject bulletPref;
    [SerializeField] float meleeAttackRange = 1f;
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<CharacterMovement>();
        stats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleChangeGun();
    }

    //�������� ������
    public WeaponScript HandleChangeGun()
    {
        WeaponScript weapon1 = null;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gunType = GunType.Pistol;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gunType = GunType.Knife;
        }

        foreach (var weapon in weapons)
        {
            if (weapon.GunType == gunType)
            {
                weapon1 = weapon;
            }
        }

        return weapon1;
    }

    //�������� ������� ���
    public void MeleeAttack()
    {
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

    //������������ ������ �����
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(meleeAttackPoint.position, meleeAttackRange);
    }

    public void Bullet()
    {
        Instantiate(HandleChangeGun().ProjectilePrefab, transform.position + move.GetShootingDirection(), Quaternion.identity);
        HandleChangeGun().Ammo -= 0.5f;
    }

    void Reload()
    {

    }
}
