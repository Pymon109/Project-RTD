using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWork_Damage : SkillWork
{
    [SerializeField]
    int _dmg_scalse;
    [SerializeField]
    float _duration;
    [SerializeField]
    int _count;

    public void SetSkillWork_Damage(int dmgScale, float duration, int count)
    {
        _dmg_scalse = dmgScale;
        _duration = duration;
        _count = count;
    }

    IEnumerator DoDamage()
    {
        int count = _count + 1;
        if(_target)
        {
            if (_targets.Count <= 0)
                _targets.Add(_target);
        }
        if(_targets.Count > 0)
        {
            if (_targets[0])
            {
                Skill skill = transform.parent.GetComponent<Skill>();
                if(skill)
                {
                    Vector3 pos = _targets[0].transform.position;
                    pos.y = 5;
                    GameManager.instance.effectManager.CreateSkillEffect(pos, skill.GetSkillID());
                }
                    
            }
            while (count > 0)
            {
                count--;
                for (int i = 0; i < _targets.Count; i++)
                {
                    int realDMG = (int)(_dmg_scalse * (1 + GameManager.instance.teamManager.GetLevel(_team) * 0.1));
                    if(_targets[i])
                    {
                        Monster targetMonster = _targets[i].GetComponent<Monster>();
                        if (_property.IsSameProperty(targetMonster.GetProperty()))
                        {
                            realDMG = (int)(realDMG * 1.5f);
                        }
                        targetMonster.Hit((int)(realDMG), _property);
                    }
                }
                yield return new WaitForSeconds(_duration / (_count+1));
            }
            _targets.Clear();
        }
        yield return null;
    }

    override public void Casting()
    {
        StartCoroutine(DoDamage());
    }

    private void Awake()
    {
        _work_type = E_WORK_TYPE.DAMAGE;
    }

    private void Start()
    {
        _team = _parent_unit.GetTeam();
        _property = _parent_unit.GetProperty();
    }

    private void Update()
    {
        _target = _parent_unit.GetTarget();
    }
}
