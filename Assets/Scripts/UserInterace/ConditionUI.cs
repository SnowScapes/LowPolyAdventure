using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionUI : MonoBehaviour
{
    [SerializeField] private Image conditionUiImage;

    public void SetFill(ConditonArgs args)
    {
        conditionUiImage.fillAmount = args.curHunger / args.maxHunger;
    }
}
