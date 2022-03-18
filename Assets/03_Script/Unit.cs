using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] UnitManager.E_Rank _rank;
    public UnitManager.E_Rank GetRank() { return _rank; }

    [SerializeField] int _sid;
    public int GetSID() { return _sid; }

    [SerializeField] string _name;
    public string GetName() { return _name; }

    [SerializeField] int _ATK;
    public int GetATK() { return _ATK; }
    float _ExtraATK_rate = 0;
    public void SetExtraATK_rate(float extra) 
    { 
        _ExtraATK_rate += extra;
        if (_ExtraATK_rate < 0)
            _ExtraATK_rate = 0;
    }

    [SerializeField] int _additional_ATK;
    int GetDmg()
    {
        int dmg = (int)((_ATK * (1 + _ExtraATK_rate)) + GameManager.instance.teamManager.GetLevel(_team) * _additional_ATK);

        int criticalHitNext = Random.RandomRange(0, 100);
        if(criticalHitNext <= (_criticalHitChanceRate + _ExtraCriticalHitChanceRate) * 100)
        {
            dmg = (int)(dmg * _criticalATK);
        }
        return dmg;
    }


    float _criticalHitChanceRate = 0.05f;
    float _ExtraCriticalHitChanceRate = 0f;
    public void SetExtraCriticalRate(float extraCriticalHitChanceRate) { _ExtraCriticalHitChanceRate += extraCriticalHitChanceRate; }
    float _criticalATK = 1.5f;


    [SerializeField] float _atkSpeed;
    float _extraATKSpeed = 0f;
    public void SetExtraATKSpeed(float atkSpeed) { _extraATKSpeed += atkSpeed; }
    public float GetATKspeed() { return _atkSpeed + _extraATKSpeed; }

    [SerializeField] float _site;

    [SerializeField] float _site_coeff = 1.7f;

    float RealSite() { return _site * _site_coeff; }

    [SerializeField] TeamManager.E_TEAM _team;
    public TeamManager.E_TEAM GetTeam() { return _team; }

    Property _property;
    public Property GetProperty() { return _property; }

    [SerializeField] ParticleSystem _effectSite;

    float _rotationSpeed = 500;

    [SerializeField] int _sellPoint;
    public int GetSellPoint() { return _sellPoint; }

    [SerializeField] Skill[] _skills = new Skill[2];
    public Skill[] GetSkills() { return _skills; }
    public void SetSkills(Skill skill01, Skill skill02)
    {
        _skills[0] = skill01;
        _skills[1] = skill02;
    }
    public void ReduceSkillLeftCoolTime(float amount)
    {
        for(int i =0; i< _skills.Length; i++)
            if(_skills[i].GetCoolTime() > 0)
                _skills[i].ReduceLeftCoolTime(amount);
    }

    [SerializeField] BuffDebuffSlot _buffslot;
    public void SetBuffDebuffSlot(BuffDebuffSlot slot) { _buffslot = slot; }
    public BuffDebuffSlot GetBuffDebuffSlot() { return _buffslot; }

    public void EffectOnSite(bool on){  _effectSite.gameObject.SetActive(on);   }

    public GameObject _target = null;
    public GameObject GetTarget() { return _target; }

    bool _isLocked = false;
    public void Switchㅣock(bool on) { _isLocked = on; }
    public bool IsLocked() { return _isLocked; }

    public UnitSounds _sounds;
    public void SetUnitSounds(UnitSounds unitSounds) { _sounds = unitSounds; }

    public void SetData(int sid, string name, UnitManager.E_Rank rank, TeamManager.E_TEAM team, Property property, int atk, int atk_adittional,
            float atk_speed, float site, int sellPoint, GameObject effectSite, E_ATTACK_TYPE attack_type)
    {
        _sid = sid;
        _name = name;
        _rank = rank;
        _team = team; ;
        _property = property;
        _ATK = atk;
        _additional_ATK = atk_adittional;
        _atkSpeed = atk_speed;
        _site = site;
        _sellPoint = sellPoint;

        _effectSite = effectSite.GetComponent<ParticleSystem>();
        _effectSite.startSize *= RealSite();

        _attackType = attack_type;

    }
    [SerializeField] Animator _aniCon;
    public void SetAniCon(Animator aniCon) { _aniCon = aniCon; }
    public void SetSkillAni(int skillSID)
    {
        _aniCon.SetInteger("SID_Skill", skillSID);
    }
    public enum E_UNITSTATE
    {
        SPAWN = 0,
        IDLE,
        NORMAL_ATTACK,
        SKILL_ATTACK
    }
    E_UNITSTATE _currentState = E_UNITSTATE.SPAWN;

    void UpdateUnitState()
    {
        switch (_currentState)
        {
            case E_UNITSTATE.SPAWN:
                break;
            case E_UNITSTATE.IDLE:
                break;
            case E_UNITSTATE.NORMAL_ATTACK:
                if (!_target)
                    SetUnitState(E_UNITSTATE.IDLE);
                break;
            case E_UNITSTATE.SKILL_ATTACK:
                if (!_target)
                    SetUnitState(E_UNITSTATE.IDLE);
                break;
        }
    }

    public void SetUnitState(E_UNITSTATE command)
    {
        switch(command)
        {
            case E_UNITSTATE.SPAWN:
                break;
            case E_UNITSTATE.IDLE:
                break;
            case E_UNITSTATE.NORMAL_ATTACK:
                
                break;
            case E_UNITSTATE.SKILL_ATTACK:
                break;
        }
        _currentState = command;
        _aniCon.SetInteger("AniState", (int)command);
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(Attack());
        SetUnitState(E_UNITSTATE.IDLE);
    }

    public void DeleteThisUnit()
    {
        GameManager.instance.teamManager.DeleteUnit(_team, _rank);
        for (int i = 0; i < _skills.Length; i++)
        {
            if (_skills[i])
            {
                if (_skills[i].GetTriggerType() == SkillManager.E_SKILL_TRIGGER.ROUND)
                {
                    int index = _skills[i].GetIndex();
                    if (index >= 0)
                        GameManager.instance.skillManager.DeleteRoundSkill(_skills[i]);
                }
            }
        }
        //UnitObjectPool unitPool = (UnitObjectPool)ObjectPoolManager.instance.m_pools[(int)ObjectPoolManager.E_POOL_TYPE.UNIT];
        UnitObjectPool unitPool = (UnitObjectPool)GameManager.instance.objectPoolManager.
            m_pools[(int)ObjectPoolManager.E_POOL_TYPE.UNIT];
        unitPool.GetPool(_sid).ReturnObject(gameObject);
        //Destroy(this.gameObject);
    }

    void ProcessFindTarget()
    {
        if(_currentState != E_UNITSTATE.SPAWN)
        {
            int nLayer = 1 << LayerMask.NameToLayer("Monster");
            Collider[] collider = Physics.OverlapSphere(transform.position, RealSite() * 10, nLayer);
            if (collider.Length > 0)
            {
                _target = collider[0].gameObject;
            }
            else
            {
                nLayer = 1 << LayerMask.NameToLayer("GoldMonster");
                collider = Physics.OverlapSphere(transform.position, RealSite() * 10, nLayer);
                if (collider.Length > 0)
                    _target = collider[0].gameObject;
                else
                    _target = null;
            }
        }
    }

    public enum E_ATTACK_TYPE
    {
        MELEE = 0,
        GUN,
        RANGED,

        NONE = -1
    }
    public E_ATTACK_TYPE _attackType = E_ATTACK_TYPE.NONE;

    IEnumerator Attack()
    {
        while(true)
        {
            if (!_target)
            {
                SetUnitState(E_UNITSTATE.IDLE);
                //break;
            }
            else
            {
                float castingTime = 0;
                if (Attack_Skill(ref castingTime))
                {
                    //스킬 공격
                    yield return new WaitForSeconds(castingTime);
                }
                else
                {
                    //일반 공격
                    SetUnitState(E_UNITSTATE.NORMAL_ATTACK);
                    Monster targetMonster = _target.GetComponent<Monster>();

                    switch (_attackType)
                    {
                        case E_ATTACK_TYPE.MELEE:
                            targetMonster.Hit((int)GetDmg(), _property);
                            _sounds.PlayNormalAttackAudio(E_ATTACK_TYPE.MELEE);
                            break;
                        case E_ATTACK_TYPE.GUN:
                            targetMonster.Hit((int)GetDmg(), _property);
                            GameManager.instance.effectManager.CreateGunEffect(_target.transform.position, _sid % 100);
                            _sounds.PlayNormalAttackAudio(E_ATTACK_TYPE.GUN);
                            break;
                        case E_ATTACK_TYPE.RANGED:
                            Vector3 startPos = transform.position;
                            startPos.y += 5;
                            GameManager.instance.effectManager.CreateTrackingAttackEffect(startPos, _sid % 100,
                                (int)GetDmg(), _property, _target.GetComponent<Monster>());
                            _sounds.PlayNormalAttackAudio(E_ATTACK_TYPE.RANGED);
                            break;
                        case E_ATTACK_TYPE.NONE:
                            targetMonster.Hit((int)GetDmg(), _property);
                            break;
                    }
                    //일반 공격 확률 스킬 여부
                    for (int i = 0; i < _skills.Length; i++)
                    {
                        if (_skills[i])
                        {
                            if (_skills[i].GetTriggerType() == SkillManager.E_SKILL_TRIGGER.ATTACK)
                            {
                                if (_skills[i].IsNormalAttackTrigger())
                                {
                                    _skills[i].SetTarget(_target);
                                    _skills[i].CheckAndCastOn();
                                }
                            }
                            else if (_skills[i].GetTriggerType() == SkillManager.E_SKILL_TRIGGER.BUFF)
                            {

                                if (_skills[i].IsDebuffTrigger())
                                {
                                    int countOfDebuff = _target.GetComponent<Monster>().GetBuffDebuffSlot().CountOfBuff(false);
                                    if (countOfDebuff > 0)
                                    {
                                        _skills[i].SetTarget(_target);
                                        _skills[i].CheckAndCastOn();
                                    }
                                }
                            }
                        }
                    }

                    yield return new WaitForSeconds(2.0f / _atkSpeed);
                }
            }
            yield return null;
        }
    }

    bool Attack_Skill(ref float castingTime)
    {
        bool isAllSkillsFree = false;
        int castSkillIndex = -1;
        for (int i = 0; i < _skills.Length; i++)
            if (_skills[i])
                if (_skills[i].GetTriggerType() == SkillManager.E_SKILL_TRIGGER.NONE)
                {
                    if(_skills[i].IsSkillCasting())
                    {
                        isAllSkillsFree = false;
                        break;
                    }
                    else
                    {
                        if (_skills[i].IsCoolTimeDown())
                        {
                            isAllSkillsFree = true;
                            castSkillIndex = i;
                            break;
                        }
                    }
                }
        if(isAllSkillsFree)
        {
            _skills[castSkillIndex].SetTarget(_target);
            _skills[castSkillIndex].SetTriggerConditionOn();

            castingTime = _skills[castSkillIndex].GetCastingTime();
        }
        else
        {
            //스킬 시전 불가 >> 일반 공격
        }

        return isAllSkillsFree;
    }

    void LookUpTarget()
    {
        if(GameManager.instance.roundManager.GetCurrentState() == RoundManager.E_ROUND_STATE.PLAY)
        {
            if (_target)
            {
                //타겟을 보게 회전
                Quaternion lookUpRotation = Quaternion.LookRotation(_target.transform.position - transform.position);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookUpRotation, Time.deltaTime * _rotationSpeed);
            }
            else
            {
                //뒤를 보게 회전
                Quaternion lookUpRotation = Quaternion.LookRotation(Vector3.forward);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookUpRotation, Time.deltaTime * _rotationSpeed);
            }
        }
        else
        {
            if (_target)
            {
                //타겟을 보게 회전
                Quaternion lookUpRotation = Quaternion.LookRotation(_target.transform.position - transform.position);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookUpRotation, Time.deltaTime * _rotationSpeed);
            }
            else
            {
                //앞을 보게 회전
                Quaternion lookUpRotation = Quaternion.LookRotation(Vector3.forward * -1f);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookUpRotation, Time.deltaTime * _rotationSpeed);
            }
        }
    }

    public void InitUnit_forCreate()
    {
        for (int i = 0; i < _skills.Length; i++)
        {
            if (_skills[i])
                if (_skills[i].GetTriggerType() == SkillManager.E_SKILL_TRIGGER.ROUND)
                    GameManager.instance.skillManager.AddRoundSkillr(_skills[i]);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, RealSite() * 10);
        //Gizmos.DrawSphere(transform.position, RealSite() * 10);
    }

    private void Awake()
    {
        //_effectSite.startSize *= _site;
        _property = GetComponent<Property>();
    }

    private void Start()
    {
        StartCoroutine(Spawn());
        //TeamManager._instance.AddUnit(_team, _rank);
    }

    private void FixedUpdate()
    {
        ProcessFindTarget();
    }

    private void Update()
    {
        UpdateUnitState();
        LookUpTarget();
        if (_target)
            if (!_target.activeInHierarchy)
                _target = null;
    }
}
