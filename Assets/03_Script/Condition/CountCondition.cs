using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CountCondition : MonoBehaviour
{
    public enum E_COUNT_CONDITION_TYPE
    {
        HOLD_TOWER = 0,
        GOLDMONSTER_LEVEL,
        USE_GOLD,
        START_ROUND,
        SPAWN_GOLDMONSTER,
        UPGRADE
    }
    protected E_COUNT_CONDITION_TYPE _conditionType;
    public E_COUNT_CONDITION_TYPE GetConditionType() { return _conditionType; }

    int _count = 0;
    public void AddCount(int amount) { _count += amount; }
    public void SetCount(int count) { _count = count; }
    public virtual int CurrentCount() { return _count; }
}
