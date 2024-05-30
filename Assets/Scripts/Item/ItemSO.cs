using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu (menuName = "ItemInfo", fileName = "Default", order = 0)]
public class ItemSO : ScriptableObject
{
    public Define.ItemType ItemType;
    public string ItemName;
    public int Value;
    public string Description;
}
