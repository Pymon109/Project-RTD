using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    static SkillManager _unique;
    public static SkillManager _instance { get { return _unique; } }

    public enum E_SKILL_TRIGGER
    {
        NONE = 0,
        SELL,
        ROUND,
        ATTACK,
        CONTINUE,
        TOWER,
        BUFF
    }

    List<Skill> _roundSkills = new List<Skill>();
    public void AddRoundSkillr(Skill skill)
    {
        _roundSkills.Add(skill);
        skill.SetIndex(_roundSkills.Count - 1);
    }
    public void DeleteRoundSkill(Skill skill)
    {
        //_roundSkills.RemoveAt(idx);
        _roundSkills.Remove(skill);
    }

    public void RoundSkillCastOn()
    {
        for (int i = 0; i < _roundSkills.Count; i++)
            _roundSkills[i].CheckAndCastOn();
    }

    private void Awake()
    {
        _unique = this;
    }
}
