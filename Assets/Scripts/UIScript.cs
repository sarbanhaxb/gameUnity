using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIScript : MonoBehaviour
{

    CharacterStats stats;

    [Header ("Bar")]
    [SerializeField] Slider hpBar;
    [SerializeField] Slider armorBar;
    [SerializeField] Slider expBar;

    [Header ("Stats")]
    [SerializeField] TMP_Text lvlText;
    [SerializeField] TMP_Text ammoText;
    [SerializeField] TMP_Text moneyText;

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindAnyObjectByType<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        InitializeStats();
    }

    void InitializeStats()
    {
        hpBar.value = stats.HP;
        armorBar.value = stats.Armor;
        expBar.value = stats.Experience;
        lvlText.text = stats.Level.ToString();
        ammoText.text = stats.Ammo+"/"+stats.MaxAmmo;
        moneyText.text = stats.Money+"$";
    }
}
