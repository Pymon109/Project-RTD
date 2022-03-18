using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountCondition_GoldMonsterLevel : CountCondition
{
    public override int CurrentCount()
    {
        return GameManager.instance.goldMonsterManaer.GetNextLevel() - 1;
    }

    private void Start()
    {
        _conditionType = E_COUNT_CONDITION_TYPE.GOLDMONSTER_LEVEL;
    }
}
