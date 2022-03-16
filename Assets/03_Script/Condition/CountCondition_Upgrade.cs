using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountCondition_Upgrade : CountCondition
{
    private void Start()
    {
        _conditionType = E_COUNT_CONDITION_TYPE.UPGRADE;
    }
}
