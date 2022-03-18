using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountCondition_StartRound : CountCondition
{
    public override int CurrentCount()
    {
        return GameManager.instance.roundManager.GetCurrentRound() + 1;
    }

    private void Start()
    {
        _conditionType = E_COUNT_CONDITION_TYPE.START_ROUND;
    }
}
