using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="new item", menuName ="Items")]

public class ItemScript : ScriptableObject
{
    public ItemType itemType;
    new public string name;
    public int value;
    public int cost;
    public Sprite sprite;
    public GameObject itemPrefab;
}
