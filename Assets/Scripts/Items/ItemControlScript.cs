using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControlScript : MonoBehaviour
{
    public ItemScript item;
    CharacterStats playerStats;
    void Start()
    {
        playerStats = FindObjectOfType<CharacterStats>();
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
                    playerStats.Experience += item.value;
                    break;
                case ItemType.BULLETS:
                    playerStats.Armor += item.value;
                    break;
            }
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
}
