using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IUseable
{
    public ItemSO ItemInfo;
    [SerializeField] protected PlayerCondition pCondition;
    
    protected float value;

    public virtual void UseItem()
    {
        Use();
    }
    
    public void Use()
    {
        value = ItemInfo.Value;
    }
}