using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int HP { set; get; }
    public int MaxHP { set; get; } = 100;
    public int Experience { set; get; } = 0;
    public int MaxExperience { set; get; } = 1000;
    public int Level { set; get; } = 1;
    public int Armor { set; get; }
    public int MaxArmorHP {get; set; }
    public int Money { set; get; } = 0;


    CharacterStats()
    {
        this.HP = this.MaxHP;
        //this.Ammo = this.MaxAmmo;
        this.Armor = this.MaxArmorHP;
    }
}
