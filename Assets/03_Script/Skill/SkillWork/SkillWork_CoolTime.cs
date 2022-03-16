using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWork_CoolTime : SkillWork
{
    [SerializeField]
    float _coolTimeReduction;

    override public void Casting()
    {
        _parent_unit.ReduceSkillLeftCoolTime(_coolTimeReduction);
    }

}
