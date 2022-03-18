using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] int _sid;

    [SerializeField] protected Transform _pos_dmgEffect;
    [SerializeField] protected GameObject _prefap_dmgEffect;
    [SerializeField] GameObject _prefap_goldEffect;
    [SerializeField] protected GUI_StatusBar _guiStatusBar;
    [SerializeField] protected GUI_StatusBar _guiStatusBar_prefab;
    public void SetStatusBar(GUI_StatusBar guiStatusBar) 
    { 
        _guiStatusBar = guiStatusBar;
        _HP = _maxHP;
        _guiStatusBar.SetState(_HP, _maxHP);
    }

    [SerializeField] string _name;
    public string GetName() { return _name; }

    [SerializeField] Sprite _img_portrait;
    public Sprite GetPortrait() { return _img_portrait; }

    [SerializeField] protected bool _isBoss;
    public bool IsBoss() { return _isBoss; }

    enum E_MONSTER_TYPE
    {
        NORMAL = 0,
        BOSS,
        GOLD
    }
    [SerializeField] E_MONSTER_TYPE _monsterType;

    protected int _level = 1;
    public void SetLevel(int level)
    {
        _level = level;
        float constF = 0f;
        switch(_monsterType)
        {
            case E_MONSTER_TYPE.NORMAL:
                constF = 1.1319f;
                _maxHP = (int)(214 * Mathf.Pow(constF, _level) - 226.5605);
                break;
            case E_MONSTER_TYPE.BOSS:
                break;
            case E_MONSTER_TYPE.GOLD:
                constF = 1.3f;
                _maxHP = (int)(1705 * Mathf.Pow(constF, _level) - 1516);
                break;
        }
        _HP = _maxHP;
        _guiStatusBar.SetState(_HP, _maxHP);
    }

    [SerializeField] protected Property _property;
    public Property GetProperty() { return _property; }

    [SerializeField] int _ATK;
    public void Attack() 
    {
        GameManager.instance.player.Hit(_ATK); 
    }

    [SerializeField] int _DEF;
    public int GetDEF() { return _DEF; }
    int _constant_DEF = 250;
    int _extra_DEF = 0;
    public void SetExtraDEF(int extra) { _extra_DEF += extra; }

    protected float GetDEFChance()
    {
        float value = 0;
        int fullDEF = _DEF + _extra_DEF;
        if (fullDEF > 0)
        {
            value = (float)fullDEF / ((float)fullDEF + (float)_constant_DEF);
        }
        return value;
    }

    [SerializeField] protected int _maxHP;
    protected int _HP;
    public int GetMaxHP() { return _maxHP; }

    [SerializeField] float _REG;
    float _extraREG = 0;
    public void SetExtraREG(float extra) { _extraREG += extra; }
    public float GetRealREG(Property property)
    {
        float value = _REG + _extraREG;
        if (value < 0)
            value = 0;
        else if (value > 1)
            value = 1f;

        if (_property.IsSameProperty(property))
            value = 0;

        return value;
    }

    virtual public void Hit(int dmg, Property property)
    {
        if (!gameObject.activeInHierarchy)
            return;

        int realDMG = (int)(dmg * (1 - GetDEFChance() - GetRealREG(property)));
        _HP -= realDMG;
        GameManager.instance.effectManager.CreateTextEffect(_pos_dmgEffect.position, realDMG.ToString(),property);
        if (_HP <= 0)
        {
            //골드 드랍
            if (_gold > 0)
                GoldDrop();
            else
                GameManager.instance.effectManager.CreateMonsterDeathEffect(transform.position);
            if(gameObject.tag == "GoldMonster")
                GameManager.instance.goldMonsterManaer.IncreaseLevel();
            Death();
        }
        else
            _guiStatusBar.SetState(_HP, _maxHP);
    }

    [SerializeField] int _gold;
    protected void GoldDrop()
    {
        GameManager.instance.player.AddGold(_gold);
        //골드 흩뿌리는 이펙트
        /*        GameObject prefap = Resources.Load<GameObject>("Prefabs/Effect/" + _prefap_goldEffect.name);
                GameObject newobj = Instantiate(prefap, _pos_dmgEffect.position, prefap.transform.rotation);*/

        GameManager.instance.effectManager.CreateGoldEffect(_pos_dmgEffect.position);
        GameManager.instance.effectManager.CreateTextEffect(_pos_dmgEffect.position, _gold.ToString(), EffectManager.E_TEXT_EFFECT_TYPE.GOLDMONSTER_DROP);
    }

    [SerializeField] BuffDebuffSlot _buffslot;
    public BuffDebuffSlot GetBuffDebuffSlot() { return _buffslot; }

    [SerializeField] MonsterObjectPool.E_MONSTER_TYPE type;
    public void Death()
    {
        _buffslot.SlotInit();
        //MonsterObjectPool monsterPool = (MonsterObjectPool)ObjectPoolManager.instance.m_pools[(int)ObjectPoolManager.E_POOL_TYPE.MONSTER];
        MonsterObjectPool monsterPool = (MonsterObjectPool)GameManager.instance.objectPoolManager.
            m_pools[(int)ObjectPoolManager.E_POOL_TYPE.MONSTER];
        if (!_isBoss)
        {
            if(_guiStatusBar)
            {
                //Destroy(_guiStatusBar.gameObject);
                monsterPool.GetPool(MonsterObjectPool.E_MONSTER_TYPE.GUI_HA_BAR).ReturnObject(_guiStatusBar.gameObject);
            }
        }
        transform.position = GameManager.instance.spawner.transform.position;
        monsterPool.GetPool(type).ReturnObject(gameObject);
    }
    public void InitMonster()

    {
        _HP = _maxHP;
        _extra_DEF = 0;
        _extraREG = 0;
        GetComponent<MonsterAI>().SetSpeedDecrease(-9999);
        transform.rotation = Quaternion.Euler(0, 180, 0);

        if (!_isBoss)
        {
            //MonsterObjectPool monsterPool = (MonsterObjectPool)ObjectPoolManager.instance.m_pools[(int)ObjectPoolManager.E_POOL_TYPE.MONSTER];
            MonsterObjectPool monsterPool = (MonsterObjectPool)GameManager.instance.objectPoolManager.
                m_pools[(int)ObjectPoolManager.E_POOL_TYPE.MONSTER];
            //GameObject hpBar = monsterPool.GetPool(MonsterObjectPool.E_MONSTER_TYPE.GUI_HA_BAR).GetObject(ObjectPoolManager.instance.trsDynamicObjects);
            GameObject hpBar = monsterPool.GetPool(MonsterObjectPool.E_MONSTER_TYPE.GUI_HA_BAR).GetObject(
                GameManager.instance.objectPoolManager.trsDynamicObjects);
            _guiStatusBar = hpBar.GetComponent<GUI_StatusBar>();
            _guiStatusBar.SetTarget(_pos_dmgEffect);
            _guiStatusBar.SetState(_HP, _maxHP);
        }

        _buffslot.SlotInit();
        transform.position = GameManager.instance.spawner.transform.position;
    }

    private void Awake()
    {
        _property = GetComponent<Property>();
    }

    private void Start()
    {
        _HP = _maxHP;
        _isBoss = false;
    }
}
