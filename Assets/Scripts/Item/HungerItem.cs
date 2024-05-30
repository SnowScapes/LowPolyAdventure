public class HungerItem : Item
{
    public override void UseItem()
    {
        base.UseItem();
        pCondition.conditionArgs.changeHungerValue = value;
        pCondition.CallHungerEvent();
        pCondition.conditionArgs.changeHungerValue = -0.05f;
    }
}