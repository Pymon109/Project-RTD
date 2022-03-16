using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTrigger_Continue : SkillTrigger
{
    private void Awake()
    {
        _trigger_type = SkillManager.E_SKILL_TRIGGER.CONTINUE;
    }
}
