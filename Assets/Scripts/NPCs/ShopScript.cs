using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public GameObject shopObj;
    public GameObject pleasePress;
    bool inShop = false;
    public List<ItemScript> items;
    public GameObject button;
    public Transform parentBtn;

    CharacterStats playerStats;


    public ItemControlScript itemControlScript;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        OpenShop();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inShop = true;
            pleasePress.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inShop = false;
            shopObj.SetActive(false);
            Clearitems();
            pleasePress.SetActive(false);
        }
    }

    void OpenShop()
    {
        if (inShop && Input.GetKeyDown(KeyCode.E))
        {
            shopObj.SetActive(true);
            GenerateItems();
        }
    }

    void GenerateItems()
    {
        foreach (var item in items)
        {
            var a = Instantiate(button, parentBtn);
            a.GetComponentInChildren<TMP_Text>().text = item.cost.ToString();
            a.GetComponentInChildren<Image>().sprite = item.sprite;

            a.GetComponent<Button>().onClick.AddListener(() => { OnButtonClick(item); });
        }
    }

    void OnButtonClick(ItemScript item)
    {
        switch (item.itemType)
        {
            case ItemType.ARMOR:
                playerStats.Armor += item.value;
                playerStats.Money -= item.cost;
                break;

            case ItemType.MEDICINE:
                playerStats.HP += item.value;
                playerStats.Money -= item.cost;
                break;
        }
    }

    void Clearitems()
    {
        int a = parentBtn.childCount;
        while (a > 0)
        {
            Destroy(parentBtn.GetChild(a - 1).gameObject);
            a--;
        }
    }
}
