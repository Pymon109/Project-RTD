using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_BottomBar : MonoBehaviour
{
    [SerializeField]
    GUI_MonsterInfo _gui_monsterInfo;
    [SerializeField]
    GUI_UnitInfo _gui_unitInfo;

    Monster _targetMonster = null;
    Unit _targetUnit = null;

    public enum E_BOTTOMBAR_STATE
    {
        NONE = 0,
        MONSTER_INFO,
        UNIT_INFO
    }
    E_BOTTOMBAR_STATE _currentState = E_BOTTOMBAR_STATE.NONE;

    void updateState()
    {
        switch(_currentState)
        {
            case E_BOTTOMBAR_STATE.NONE:
                break;
            case E_BOTTOMBAR_STATE.MONSTER_INFO:
                if(!_targetMonster)
                    SetState(E_BOTTOMBAR_STATE.NONE);
                break;
            case E_BOTTOMBAR_STATE.UNIT_INFO:
                break;
        }
    }
    public void SetState(E_BOTTOMBAR_STATE command)
    {
        switch (command)
        {
            case E_BOTTOMBAR_STATE.NONE:
                _gui_monsterInfo.gameObject.SetActive(false);
                _gui_unitInfo.gameObject.SetActive(false);
                break;
            case E_BOTTOMBAR_STATE.MONSTER_INFO:
                _gui_monsterInfo.gameObject.SetActive(true);
                _gui_unitInfo.gameObject.SetActive(false);
                if (_targetMonster)
                    _gui_monsterInfo.SetMonsterInfo(_targetMonster);
                break;
            case E_BOTTOMBAR_STATE.UNIT_INFO:
                _gui_monsterInfo.gameObject.SetActive(false);
                _gui_unitInfo.gameObject.SetActive(true);
                if (_targetUnit)
                    _gui_unitInfo.SetUnitInfo(_targetUnit);
                break;
        }
        _currentState = command;
    }


    public void SetTargetMonster(RaycastHit hit)
    {
        _targetMonster = hit.collider.gameObject.GetComponent<Monster>();
        SetState(E_BOTTOMBAR_STATE.MONSTER_INFO);
    }

    public void SetTargetUnit(RaycastHit hit)
    {
        _targetUnit = hit.collider.gameObject.GetComponent<Tile>().GetUnit();
        if(_targetUnit)
            SetState(E_BOTTOMBAR_STATE.UNIT_INFO);
        else
            SetState(E_BOTTOMBAR_STATE.NONE);
    }

    private void Update()
    {
        updateState();
    }
}
