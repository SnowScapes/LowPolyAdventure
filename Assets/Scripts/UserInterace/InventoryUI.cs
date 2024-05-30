using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    // 전략패턴 사용해보기
    private List<IUseable> items = new List<IUseable>();
    
    private void Start()
    {
        for (int i = 0; i < items.Count; i++)
        {
            
        }
    }
}
