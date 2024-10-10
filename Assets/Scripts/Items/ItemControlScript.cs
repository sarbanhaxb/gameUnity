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
                    playerStats.Armor = CheckMaxStat(playerStats.Armor, playerStats.MaxArmor, item.value);
                    break;
                case ItemType.MEDICINE:
                    playerStats.HP = CheckMaxStat(playerStats.HP, playerStats.MaxHP, item.value);
                    break;
                case ItemType.MONEY:
                    playerStats.Money += item.value;
                    break;
                case ItemType.EXPITEM:
                    LevelUp(item.value);
                    break;
                case ItemType.BULLETS:
                    Debug.Log("TUTACHKI");
                    var weapon = FindGun(character.weapons, ItemType.BULLETS);
                    weapon.MaxAmmo = CheckMaxStat(weapon.MaxAmmo, weapon.LimitAmmo, item.value);
                    break;
                case ItemType.ARROWS:
                    weapon = FindGun(character.weapons, ItemType.ARROWS);
                    weapon.MaxAmmo = CheckMaxStat(weapon.MaxAmmo, weapon.LimitAmmo, item.value);
                    break;
            }
            Destroy(gameObject);
        }
    }

    private int CheckMaxStat(int currentStat, int maxStat, int itemValue)
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
            UI.ChangeMaxExp(playerStats.MaxExperience);
        }
        else
            playerStats.Experience += value;
    }
}
