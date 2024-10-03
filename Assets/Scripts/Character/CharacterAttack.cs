using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public GunType gunType = GunType.Pistol;

    CharacterMovement move;
    CharacterStats stats;
    WeaponScript Stats;
    UIScript UI;
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
        UI = FindObjectOfType<UIScript>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleChangeGun();
        if (Input.GetKeyDown(KeyCode.R)) HandleChangeGun().Reload(gunType);
    }

    private void OnDestroy()
    {
        weapons.ForEach(weapon => weapon.ResetValues());
    }

    //изменить оружие
    public WeaponScript HandleChangeGun()
    {
        WeaponScript weapon1 = null;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gunType = GunType.Pistol;
            UI.ChangeColor(gunType);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gunType = GunType.Knife;
            UI.ChangeColor(gunType);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gunType = GunType.Crossbow;
            UI.ChangeColor(gunType);

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

    //включает ближний бой
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

    //отрисовывает радиус удара
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(meleeAttackPoint.position, meleeAttackRange);
    }

    public void Bullet()
    {
        Instantiate(HandleChangeGun().ProjectilePrefab, transform.position + move.GetShootingDirection(), Quaternion.identity);
        HandleChangeGun().Ammo -= 0.5f;
    }
}
