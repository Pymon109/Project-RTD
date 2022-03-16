﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountCondition_Unit : CountCondition
{
    [SerializeField]
    UnitManager.E_Rank _taregtRank;
    enum E_UNIT_SET_CONDITION
    {
        NONE = 0,
        SAME,
        DIFFRENT
    }
    [SerializeField]
    E_UNIT_SET_CONDITION _targetSet;

    public override int CurrentCount()
    {
        int count = 0;
        switch(_targetSet)
        {
            case E_UNIT_SET_CONDITION.NONE:
                count = UnitManager._instance.CountOfAllUnit(_taregtRank);
                break;
            case E_UNIT_SET_CONDITION.SAME:
                count = UnitManager._instance.MaxCountOfSameUnit(_taregtRank);
                break;
            case E_UNIT_SET_CONDITION.DIFFRENT:
                count = UnitManager._instance.CountOfDifferentUnit(_taregtRank);
                break;
        }
        return count;
    }

    private void Start()
    {
        _conditionType = E_COUNT_CONDITION_TYPE.HOLD_TOWER;
    }
}
