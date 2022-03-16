using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{


    [SerializeField]
    GameObject _effect_select;
    public void ActiveEffect_select(bool command)
    {
        if (_currentState == E_TileState.REACH_EPIC)
            command = false;
        _effect_select.SetActive(command);
        if(_unit)
        {
            if (_unit.IsLocked())
                command = true;
            else
                command = false;
            _effect_locked.SetActive(command);
        }
    }

    [SerializeField]
    GameObject _unitSlot;

    [SerializeField]
    GameObject _effect_locked;

    [SerializeField]
    AudioSource _audio_select;
    public void PlaySelectAudio() { _audio_select.Play(); }

    Unit _unit = null;
    public Unit GetUnit() { return _unit; }
    bool _IsSelected = false;
    //string[] _sRank = { "Normal", "Magic", "Rare", "Unique", "Epic" };

    public void SelectTile(bool command)
    {
        _IsSelected = command;
        ActiveEffect_select(command);
        if (_unit)
            _unit.EffectOnSite(command);
    }

    public enum E_TileState
    {
        NONE = 0,
        MERGEABLE,
        MERGE_NOT_ALLOWED,
        REACH_EPIC,
        COUPON_WAIT,
        COUPON_SELECT
    }
    E_TileState _currentState = E_TileState.NONE;
    public E_TileState GetTileState() { return _currentState; }

    void UpdateTileState()
    {
        switch (_currentState)
        {
            case E_TileState.NONE:
                if (Input.GetKeyDown(KeyCode.D) || TileControl._instance.m_mouseCon.DetectSelectTile())
                {
                    if (Player._instance.SpendGold(100))
                    {
                        CreateUnit();
                        SetMergeble();
                    }
                }
                break;
            case E_TileState.MERGEABLE:
                if (Input.GetKeyDown(KeyCode.D) || TileControl._instance.m_mouseCon.DetectSelectTile())
                {
                    TileControl._instance.MergeUnit();
                    SetMergeble();
                }
                OperateKeySystem();
                break;
            case E_TileState.MERGE_NOT_ALLOWED:
                OperateKeySystem();
                break;
            case E_TileState.REACH_EPIC:
                OperateKeySystem();
                break;
            case E_TileState.COUPON_WAIT:
                break;
            case E_TileState.COUPON_SELECT:
                break;
        }
    }

    public void SetTileState(E_TileState command)
    {
        switch(command)
        {
            case E_TileState.NONE:
                ActiveEffect_select(false);
                break;
            case E_TileState.MERGEABLE:
                break;
            case E_TileState.MERGE_NOT_ALLOWED:
                break;
            case E_TileState.REACH_EPIC:
                break;
            case E_TileState.COUPON_WAIT:
                ActiveEffect_select(true);
                break;
            case E_TileState.COUPON_SELECT:
                //Debug.Log(gameObject.name + "tile selected for coupon.");
                break;
        }
        _currentState = command;
    }

    void OperateKeySystem()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //유닛 팔기
            SellUnit();
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            //유닛 잠그기
            LockUnit();
        }
    }

    public void SellUnit()
    {
        if (_unit)
        {
            Player._instance.AddGold(_unit.GetSellPoint());
            //유닛 파는 이펙트
            
            Skill[] skills = _unit.GetSkills();
            for(int i = 0; i < skills.Length; i++)
            {
                if (skills[i])
                    if (skills[i].GetTriggerType() == SkillManager.E_SKILL_TRIGGER.SELL)
                    {
                        //skills[i].SetTriggerConditionOn();
                        skills[i].CheckAndCastOn();
                    }
            }
            DeleteUnit();
        }
    }

    public void LockUnit()
    {
        if (_unit)
        {
            if (_unit.IsLocked())
            {
                _unit.Switchㅣock(false);
            }
            else
            {
                _unit.Switchㅣock(true);
            }
            SetMergeble();
            TileControl._instance.EffectSwitcher(true);
        }
    }


    public void CreateUnit(int sid = -1)
    {
        int rank = 0;

        int nextID;
        int nextSID;

        if (_unit)
        {
            rank = (int)_unit.GetRank();
            rank++;
            DeleteUnit();
        }
        if(sid >= 0)
        {
            nextSID = sid;
        }
        else
        {
            nextID = Random.RandomRange(0, 6);
            nextSID = rank * 100 + nextID;
        }

        UnitObjectPool unitPool = (UnitObjectPool)ObjectPoolManager.instance.m_pools[(int)ObjectPoolManager.E_POOL_TYPE.UNIT];
        GameObject newobj = unitPool.GetPool(nextSID).GetObject(transform);
        newobj.transform.position = transform.position;
        _unit = newobj.GetComponent<Unit>();

        UnitManager._instance.AddUnitOnTile(int.Parse(gameObject.name), _unit.GetSID());
        TeamManager._instance.AddUnit(_unit.GetTeam(), _unit.GetRank());
        //_unit.InitUnit_forCreate();

        //mergeble인지 파악하기.

    }

    public void SetMergeble()
    {
        if(_unit.GetRank() != UnitManager.E_Rank.EPIC)
            if (TileControl._instance.FindSameUnit(_unit))
                TileControl._instance.EffectSwitcher(true);
            else
                SetTileState(E_TileState.MERGE_NOT_ALLOWED);
        else
            SetTileState(E_TileState.REACH_EPIC);
    }

    public void DeleteUnit()
    {
        UnitManager._instance.DeleteUnitOnTile(int.Parse(gameObject.name), _unit.GetSID());
        _unit.DeleteThisUnit();
        //Destroy(_unit.gameObject);
        _unit = null;
        SetTileState(E_TileState.NONE);
    }

    private void Start()
    {
        _audio_select.volume = 0.5f;
        _audio_select.Stop();
    }

    private void Update()
    {
        if(_IsSelected)
            UpdateTileState();
    }

}
