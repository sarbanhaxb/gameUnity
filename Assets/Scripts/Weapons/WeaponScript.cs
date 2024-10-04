using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon", menuName = "Weapons")]
public class WeaponScript : ScriptableObject
{
    public float Ammo;
    public int MaxAmmo;
    public int LimitAmmo;
    public int Magazine;
    public int Damage;
    public int AttackSpeed;
    public float ReloadTime;
    public GunType GunType;
    public GameObject ProjectilePrefab;

    private float initialAmmo;
    private int initialMaxAmmo;
    private int initialLimitAmmo;
    private int initialMagazine;
    private int initialDamage;
    private int initialAttackSpeed;
    private float initialReloadTime;

    void SaveInitialValues()
    {
        initialAmmo = Ammo;
        initialMaxAmmo = MaxAmmo;
        initialLimitAmmo = LimitAmmo;
        initialMagazine = Magazine;
        initialDamage = Damage;
        initialAttackSpeed = AttackSpeed;
        initialReloadTime = ReloadTime;
    }
    public void ResetValues()
    {
        Ammo = initialAmmo;
        MaxAmmo = initialMaxAmmo;
        LimitAmmo = initialLimitAmmo;
        Magazine = initialMagazine;
        Damage = initialDamage;
        AttackSpeed = initialAttackSpeed;
        ReloadTime = initialReloadTime;
    }
    private void OnEnable()
    {
        SaveInitialValues();
    }

    public void Reload(GunType gunType)
    {
        switch (gunType)
        {
            case GunType.Pistol:
                if (MaxAmmo > 0)
                {
                    MaxAmmo -= (int)(Magazine - Ammo);
                    Ammo = Magazine;
                }
                break;
            case GunType.Crossbow:
                {
                    if (MaxAmmo > 0)
                    {
                        MaxAmmo -= (int)(Magazine - Ammo);
                        Ammo = Magazine;
                    }
                    break;
                }
        }
    }
}
