using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatHandler : MonoBehaviour
{
    public PlayerStat stat;
    public float currentHP { get; set; }
    public float currentStamina { get; set; }
    public float currentHunger { get; set; }

    private void Start()
    {
        currentHP = stat.HP;
        currentStamina = stat.Stamina;
        currentHunger = stat.Hunger;
    }
}
