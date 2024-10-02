using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class UIScript : MonoBehaviour
{
    CharacterStats stats;
    CharacterAttack characterAttack;

    [Header ("Bar")]
    [SerializeField] Slider hpBar;
    [SerializeField] Slider armorBar;
    [SerializeField] Slider expBar;

    [Header ("Stats")]
    [SerializeField] TMP_Text lvlText;
    [SerializeField] TMP_Text ammoText;
    [SerializeField] TMP_Text moneyText;

    [Header("Gun")]
    [SerializeField] Image Pistol;
    [SerializeField] Image CrossBow;
    [SerializeField] Image Knife;


    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindAnyObjectByType<CharacterStats>();
        characterAttack = FindObjectOfType<CharacterAttack>();
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
        ammoText.text = characterAttack.HandleChangeGun().Ammo+"/"+ characterAttack.HandleChangeGun().MaxAmmo;
        moneyText.text = stats.Money+"$";
    }
}