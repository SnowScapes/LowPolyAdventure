using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ConditionUI Health;
    [SerializeField] private ConditionUI Stamina;
    [SerializeField] private ConditionUI Hunger;

    [SerializeField] private PlayerCondition condition;

    private void Start()
    {
        condition.HungerEvent += Hunger.SetFill;
    }
}
