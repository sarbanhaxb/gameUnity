using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int HP;
    public int MaxHP = 100;
    public int Experience = 0;
    public int MaxExperience = 1000;
    public int Level = 1;
    public int Armor = 5;
    public int MaxArmor = 100;
    public int Money = 0;


    CharacterStats()
    {
        //this.HP = this.MaxHP;
        //this.Ammo = this.MaxAmmo;
        //this.Armor = this.MaxArmorHP;
    }
}
