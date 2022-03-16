using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTrigger : MonoBehaviour
{
    
    protected SkillManager.E_SKILL_TRIGGER _trigger_type = SkillManager.E_SKILL_TRIGGER.NONE;
    public SkillManager.E_SKILL_TRIGGER GetTriggerType() { return _trigger_type; }

    protected bool _trigger = false;
    public bool IsTriggerActive() { return _trigger; }
    public void SetTriggerActive(bool command) { _trigger = command; }
}
