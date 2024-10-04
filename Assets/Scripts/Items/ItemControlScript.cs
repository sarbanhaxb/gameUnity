using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControlScript : MonoBehaviour
{
    public ItemScript item;
    CharacterStats playerStats;
    void Start()
    {
        playerStats = GetComponent<CharacterStats>();
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
                    playerStats.Armor += item.value;
                    break;
                case ItemType.MEDICINE:
                    playerStats.HP += item.value;
                    break;
                case ItemType.MONEY:
                    break;
            }
        }
    }

    //WeaponScript FindGun(List<WeaponScript> weapons)
    //{

         
    //}
}
