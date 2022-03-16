using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWork_BuffDebuff : SkillWork
{
    [SerializeField]
    List<Buff> _buffs = new List<Buff>();
    public void AddBuff(Buff buff) { _buffs.Add(buff); }

    override public void Casting()
    {
        Skill skill = transform.parent.GetComponent<Skill>();
        string skillID = "";
        Vector3 pos = Vector3.zero;
        if (skill)
        {
            skillID = skill.GetSkillID();
        }

        for(int i = 0; i < _buffs.Count; i++)
        {
            if (_target)
            {
                GameObject newBuff = Resources.Load<GameObject>("Prefabs/BuffDebuff/" + _buffs[i].gameObject.name);
                GameObject instance = Instantiate(newBuff);
                instance.GetComponent<Buff>().SetTeam(_team);
                instance.GetComponent<Buff>().SetProperty(_property);
                BuffDebuffSlot buffDebuffSlot = null;
                switch(_target_type)
                {
                    case E_TARGET_TYPE.MONSTER:
                        pos = _target.transform.position;
                        buffDebuffSlot = _target.GetComponent<Monster>().GetBuffDebuffSlot();
                        break;
                    case E_TARGET_TYPE.MY:
                        pos = _parent_unit.transform.position;
                        buffDebuffSlot = _target.GetComponent<Unit>().GetBuffDebuffSlot();
                        break;
                }
                pos.y = 10;
                EffectManager._instance.CreateSkillEffect(pos, skillID + "_buff");
                buffDebuffSlot.AddBuffDebuff(instance.GetComponent<Buff>());
            }
        }
        
    }

    private void Awake()
    {
        _work_type = E_WORK_TYPE.BUFF;
    }

    private void Start()
    {
        _team = _parent_unit.GetTeam();
        _property = _parent_unit.GetProperty();
    }

    private void Update()
    {
        SetTarget(_parent_unit.GetTarget());
        //_target = _parent_unit.GetTarget();
    }
}
