using System;

public class ConditonArgs
{
    public float maxHunger;
    public float curHunger;
    public float changeHungerValue = - 0.05f;
    
    public ConditonArgs(float max, float cur)
    {
        maxHunger = max;
        curHunger = cur;
    }
}
