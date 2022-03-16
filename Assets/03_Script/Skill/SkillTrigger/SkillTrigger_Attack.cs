using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTrigger_Attack : SkillTrigger
{
    [SerializeField]
    bool _normalAttak;
    public bool IsNormalAttackTrigger() { return _normalAttak; }
    [SerializeField]
    bool _skillAttack;
    public bool IsSkillAttackTrigger() { return _skillAttack; }

    private void Awake()
    {
        _trigger_type = SkillManager.E_SKILL_TRIGGER.ATTACK;
    }
}
