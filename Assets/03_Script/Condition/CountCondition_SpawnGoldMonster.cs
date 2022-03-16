using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountCondition_SpawnGoldMonster : CountCondition
{
    private void Start()
    {
        _conditionType = E_COUNT_CONDITION_TYPE.SPAWN_GOLDMONSTER;
    }
}
