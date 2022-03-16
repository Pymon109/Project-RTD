using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountCondition_UseGold : CountCondition
{
    private void Start()
    {
        _conditionType = E_COUNT_CONDITION_TYPE.USE_GOLD;
    }
}
