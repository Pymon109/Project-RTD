using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCondition_HP : ActiveCondition
{
    [SerializeField]
    int _targetHP;
    public override bool ActiveTriger()
    {
        if (Player._instance.GetHP() <= _targetHP)
            return true;
        else
            return false;
        
    }
}
