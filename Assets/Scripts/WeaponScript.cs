using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon", menuName ="Weapons")]
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
}
