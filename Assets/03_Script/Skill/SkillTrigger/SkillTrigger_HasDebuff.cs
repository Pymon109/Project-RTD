using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTrigger_HasDebuff : SkillTrigger
{
    [SerializeField]
    bool _isBuff;
    public bool IsDebuffTrigger() { return !_isBuff; }

    private void Awake()
    {
        _trigger_type = SkillManager.E_SKILL_TRIGGER.BUFF;
    }
}
