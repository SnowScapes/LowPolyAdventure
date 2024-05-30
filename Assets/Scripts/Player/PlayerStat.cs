using System;
using UnityEngine;

[Serializable]
public class PlayerStat
{
    [SerializeField] private float hp;
    [SerializeField] private float stamina;
    [SerializeField] private float hunger;
    [SerializeField] private int speed;
    [SerializeField] private int jump;
    
    public float HP {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
        }
    }

    public float Stamina
    {
        get
        {
            return stamina;
        }
        set
        {
            stamina = value;
        }
    }

    public float Hunger
    {
        get
        {
            return hunger;
        }
        set
        {
            hunger = value;
        }
    }

    public int Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    public int Jump
    {
        get
        {
            return jump;
        }
        set
        {
            jump = value;
        }
    }
}
