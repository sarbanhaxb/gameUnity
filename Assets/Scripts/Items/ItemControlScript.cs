using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControlScript : MonoBehaviour
{
    public ItemScript item;
    CharacterStats playerStats;
    CharacterAttack character;
    UIScript UI;
    void Start()
    {
        playerStats = FindObjectOfType<CharacterStats>();
        character = FindObjectOfType<CharacterAttack>();
        UI = FindObjectOfType<UIScript>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            switch (item.itemType)
            {
                case ItemType.ARMOR:
                    if(playerStats.Armor < playerStats.MaxHP)
                    {
                        playerStats.Armor = CheckMaxStat(playerStats.Armor, playerStats.MaxArmor, item.value);
                        Destroy(gameObject);
                    }
                    break;
                case ItemType.MEDICINE:
                    if (playerStats.HP < playerStats.MaxHP)
                    {
                        playerStats.HP = CheckMaxStat(playerStats.HP, playerStats.MaxHP, item.value);
                        Destroy(gameObject);
                    }
                    break;
                case ItemType.MONEY:
                    playerStats.Money += item.value;
                    Destroy(gameObject);
                    break;
                case ItemType.EXPITEM:
                    LevelUp(item.value);
                    Destroy(gameObject);

                    break;
                case ItemType.BULLETS:
                    var weapon = FindGun(character.weapons, ItemType.BULLETS);
                    if (weapon.MaxAmmo < weapon.LimitAmmo)
                    {
                        weapon.MaxAmmo = CheckMaxStat(weapon.MaxAmmo, weapon.LimitAmmo, item.value);
                        Destroy(gameObject);
                    }
                    break;
                case ItemType.ARROWS:
                    weapon = FindGun(character.weapons, ItemType.ARROWS);
                    {
                        weapon.MaxAmmo = CheckMaxStat(weapon.MaxAmmo, weapon.LimitAmmo, item.value);
                        Destroy(gameObject);
                    }
                    break;
            }
        }
    }

    public int CheckMaxStat(int currentStat, int maxStat, int itemValue)
    {
        if (currentStat <= maxStat && currentStat + itemValue >= maxStat)
        {
            return maxStat;
        }
        else
        {
            return currentStat + itemValue;
        }
    }

    private WeaponScript FindGun(List<WeaponScript> weapons, ItemType itemType)
    {
        foreach (var weapon in weapons)
        {
            if (weapon.bulletType == itemType)
            {
                return weapon;
            }
        }
        return weapons[0];
    }
    
    public void LevelUp(int value)
    {
        if (playerStats.Experience >= playerStats.MaxExperience || playerStats.Experience + value >= playerStats.MaxExperience)
        {
            playerStats.Experience += value;
            playerStats.Level++;
            playerStats.Experience -= playerStats.MaxExperience;
            playerStats.MaxExperience = (int)(playerStats.Level * 0.5f * (playerStats.MaxExperience + (playerStats.MaxExperience * 0.1f)));
            UpgradeStatse();
            UI.ChangeMaxExp(playerStats.MaxExperience);
        }
        else
            playerStats.Experience += value;
    }

    void UpgradeStatse()
    {
        float multiplier = Mathf.Pow(playerStats.Level, 0.2f);
        playerStats.MaxHP = (int)(multiplier * playerStats.MaxHP);
        playerStats.MaxArmor = (int)(multiplier * playerStats.MaxArmor);
    }
}
