using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_SkillIcon : MonoBehaviour
{
    [SerializeField]
    Skill _skill;
    public void SetSkill(Skill skill) { _skill = skill; }
    public Skill GetSkill() { return _skill; }
    public void InitSkill() { _skill = null; }
}
