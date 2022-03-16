using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTrigger_Sell : SkillTrigger
{
    
    private void Awake()
    {
        _trigger_type = SkillManager.E_SKILL_TRIGGER.SELL;
    }
}
