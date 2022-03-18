using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField]
    int _sid;
    public int GetSkillSID() { return _sid; }
    [SerializeField]
    string _skillID;
    public string GetSkillID() { return _skillID; }
    [SerializeField]
    string _name;
    public string GetName() { return _name; }
    [SerializeField]
    string _comment;
    public string GetComment() { return _comment; }
    [SerializeField]
    SkillTrigger _trigger_condition;
    public SkillManager.E_SKILL_TRIGGER GetTriggerType() { return _trigger_condition.GetTriggerType(); }
    public void SetTrigger(SkillTrigger trigger) { _trigger_condition = trigger; }
    public void SetTriggerConditionOn() { _trigger_condition.SetTriggerActive(true); }

    [SerializeField]
    float _cast_chance;
    [SerializeField]
    float _coolTime;
    public float GetCoolTime() { return _coolTime; }
    float _leftCoolTime;
    public void ReduceLeftCoolTime(float amount)
    {
        _leftCoolTime -= amount;
    }
    bool _isCoolTimeDown = true;
    public bool IsCoolTimeDown() { return _isCoolTimeDown; }

    [SerializeField]
    float _castingTime;
    public float GetCastingTime() { return _castingTime; }

    bool _isSkillCasting = false;
    public bool IsSkillCasting() { return _isSkillCasting; }

    [SerializeField]
    List<SkillWork> _skill_works;
    public void AddSkillWork(SkillWork work)
    {
        _skill_works.Add(work);
    }

    public void SetSkillData(int sid, string skillID, string name, float cast_chance, float cooltime, float castingTime, string comment)
    {
        _sid = sid;
        _skillID = skillID;
        _name = name;
        _cast_chance = cast_chance;
        _coolTime = cooltime;
        _castingTime = castingTime;
        _comment = comment;
    }

    //타겟 설정
    GameObject _target = null;
    public void SetTarget(GameObject target) 
    { 
        _target = target; 
        for(int i = 0; i < _skill_works.Count; i++)
        {
            if (_skill_works[i].GetTargetType() == SkillWork.E_TARGET_TYPE.MONSTER)
                _skill_works[i].SetTarget(_target);
        }
    }

    //트리거가 ATTACK유형일 경우를 위한 함수
    public bool IsNormalAttackTrigger()
    {
        return ((SkillTrigger_Attack)_trigger_condition).IsNormalAttackTrigger();
    }
    public bool IsSkillAttackTrigger()
    {
        return ((SkillTrigger_Attack)_trigger_condition).IsSkillAttackTrigger();
    }

    //트리거가 BUFF유형일 경우를 위한 함수
    public bool IsDebuffTrigger()
    {
        return ((SkillTrigger_HasDebuff)_trigger_condition).IsDebuffTrigger();
    }


    //SkillManager를 위한 인스턴스와 함수
    int _index = -1;
    public void SetIndex(int i) { _index = i; }
    public int GetIndex() { return _index; }


    //확률 관련 함수
    bool RandomGenerator()
    {
        if (_cast_chance >= 1)
            return true;
        int num = Random.RandomRange(1,1000);
        if (num <= _cast_chance * 1000)
            return true;
        else
            return false;
    }

    //시전부
    IEnumerator CoolTimeCycle()
    {
        _isCoolTimeDown = false;
        while (_leftCoolTime > 0)
        {
            yield return new WaitForSeconds(1f);
            --_leftCoolTime;
        }
        _leftCoolTime = _coolTime;
        _isCoolTimeDown = true;
    }

    public void CheckAndCastOn()
    {
        if(RandomGenerator())
        {
            if (_coolTime > 0)
            {
                if (_isCoolTimeDown)
                {
                    if (_castingTime > 0)
                        StartCoroutine(Cast());
                    //Debug.Log(_name + " is active.");
                    StartCoroutine(CoolTimeCycle());
                }
            }
            else
            {
                CastOn();
            }
        }
    }

    IEnumerator Cast()  //시전 시간이 있는 경우
    {
        _isSkillCasting = true;
        //Debug.Log("cast a skill (" + _name + ")");
        for (int i = 0; i < _skill_works.Count; i++)
            _skill_works[i].Casting();
        yield return new WaitForSeconds(_castingTime);
        _isSkillCasting = false;

    }

    void CastOn()   //시전 시간이 없는 경우 (활성화 스킬같은거)
    {
        for (int i = 0; i < _skill_works.Count; i++)
            _skill_works[i].Casting();
    }


    //콜백
    private void Start()
    {
        _leftCoolTime = _coolTime;
        if(GetTriggerType() == SkillManager.E_SKILL_TRIGGER.ROUND)
        {
            GameManager.instance.skillManager.AddRoundSkillr(this);
        }
    }

    private void Update()
    {
        if(_trigger_condition.IsTriggerActive())
        {
            if(_coolTime > 0)
            {
                if(_isCoolTimeDown)
                {
                    if (_castingTime > 0)
                        StartCoroutine(Cast());
                    else
                        CastOn();
                    //Debug.Log(_name + " is active.");
                    Unit unit = transform.parent.transform.parent.GetComponent<Unit>();
                    unit.SetUnitState(Unit.E_UNITSTATE.SKILL_ATTACK);
                    unit.SetSkillAni(_sid);
                    StartCoroutine(CoolTimeCycle());
                }
            }
        }
    }
}
