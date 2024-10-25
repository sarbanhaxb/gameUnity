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


    public bool isReloading = false;
    float currentReloadingTime = 0;

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
        if (Input.GetKeyDown(KeyCode.R) && HandleChangeGun().Ammo < HandleChangeGun().Magazine) isReloading = true;
        if (isReloading)
        {
            Reloading();
        }
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
        var eminem = enemy.GetComponent<EnemyScript>();
        Debug.Log(eminem.name);
        if (enemy != null && enemy.tag == "Enemy")
        {
            eminem.EnemyHP -= 10;
            if (eminem.EnemyHP <= 0)
            {
                var anim = eminem.gameObject.GetComponent<Animator>();
                anim.SetTrigger("DeathTrigger");

                eminem.EnemySpeed = 0;
                Destroy(eminem.gameObject, 20);
                eminem.DropGoods();
                eminem.currentState = EnemyScript.ZombieState.Death;
                eminem.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            }
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


    void Reloading()
    {
        if (currentReloadingTime < HandleChangeGun().ReloadTime)
        {
            currentReloadingTime += Time.deltaTime;
            UI.ReloadProgress(currentReloadingTime);
        }
        else
        {
            currentReloadingTime = 0;
            Recharged();
        }
    }

    void Recharged()
    {
        var weapon = HandleChangeGun();
        int dif = (int)(weapon.Magazine - weapon.Ammo);
        if (weapon.MaxAmmo > dif)
        {
            weapon.MaxAmmo -= dif;
            weapon.Ammo += dif;
        }
        else
        {
            weapon.Ammo += weapon.MaxAmmo;
            weapon.MaxAmmo = 0;
        }
        isReloading = false;
    }
}
