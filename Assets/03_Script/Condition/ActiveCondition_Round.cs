using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCondition_Round : ActiveCondition
{
    [SerializeField]
    int _targetRound;

    public override bool ActiveTriger()
    {
        if ((GameManager.instance.roundManager.GetCurrentRound() + 1) == _targetRound)
            return true;
        else
            return false;
    }
}
