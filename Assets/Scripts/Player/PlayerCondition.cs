using System;
using System.Collections;
using UnityEngine;
using Object = System.Object;

public class PlayerCondition : MonoBehaviour
{
    public event Action<ConditonArgs> HealthEvent;
    public event Action<ConditonArgs> HungerEvent;
    public event Action<ConditonArgs> StaminaEvent; 
    
    public ConditonArgs conditionArgs;
    
    [SerializeField] public PlayerStatHandler stathandler;

    private void Start()
    {
        conditionArgs = new ConditonArgs(stathandler.stat.Hunger, stathandler.currentHunger);
        HungerEvent += reduceHunger;
        StartCoroutine(reduceHungerCouroutine());
    }

    public void CallHungerEvent()
    {
        HungerEvent?.Invoke(conditionArgs);
    }

    public void CallHealthEvent()
    {
        HealthEvent?.Invoke(conditionArgs);
    }

    public void callStaminaEvent()
    {
        StaminaEvent?.Invoke(conditionArgs);
    }

    private void reduceHunger(ConditonArgs args)
    {
        stathandler.currentHunger = stathandler.currentHunger < 0 ? 0 : stathandler.currentHunger + args.changeHungerValue;
        args.curHunger = stathandler.currentHunger;
    }

    private IEnumerator reduceHungerCouroutine()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        while (true)
        {
            CallHungerEvent();
            yield return delay;
        }
    }
}