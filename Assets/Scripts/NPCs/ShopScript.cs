using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public GameObject shopObj;
    bool inShop = false;
    public List<ItemScript> items;
    public GameObject button;
    public Transform parentBtn;

    // Start is called before the first frame update
    void Start()
    {

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
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inShop = false;
            shopObj.SetActive(false);
            Clearitems();
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
            Instantiate(button, parentBtn);
            button.GetComponentInChildren<TMP_Text>().text = item.cost.ToString();
            button.GetComponentInChildren<Image>().sprite = item.sprite;
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
