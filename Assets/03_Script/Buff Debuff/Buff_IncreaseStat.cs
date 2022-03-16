using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_IncreaseStat : Buff
{
    [SerializeField]
    int _increaseAmount;

    [SerializeField]
    float _increaseRate;

    enum E_STAT
    {
        DEF,
        REG,
        ATK,
        DMG_SKILL,
        CRITICAL_CHANCE,
        ATTACK_SPEED,
        MOVESPEED
    }

    [SerializeField]
    E_STAT _statType;

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(_duration);
        BuffOff();
    }

    override public void BuffOn()
    {
        if (_target)
        {
            Unit targetUnit = _target.GetComponent<Unit>();
            if (targetUnit)
            {
                switch (_statType)
                {
                    case E_STAT.ATK:
                        targetUnit.SetExtraATK_rate(_increaseRate);
                        break;
                    case E_STAT.DMG_SKILL:
                        
                        break;
                    case E_STAT.CRITICAL_CHANCE:
                        targetUnit.SetExtraCriticalRate(_increaseRate);
                        break;
                    case E_STAT.ATTACK_SPEED:
                        targetUnit.SetExtraATKSpeed(_increaseRate);
                        break;
                }
            }
            else
            {
                Monster targetMonster = _target.GetComponent<Monster>();
                if(targetMonster)
                {
                    switch (_statType)
                    {
                        case E_STAT.DEF:

                            break;
                        case E_STAT.REG:

                            break;
                        case E_STAT.MOVESPEED:

                            break;
                    }
                }
            }
            StartCoroutine(CountDown());
        }
        else
        {
            Debug.LogError(this + "target is null");
        }
    }
    override public void BuffOff()
    {
        if (_target)
        {
            Unit targetUnit = _target.GetComponent<Unit>();
            if (targetUnit)
            {
                switch (_statType)
                {
                    case E_STAT.ATK:
                        targetUnit.SetExtraATK_rate(_increaseRate * -1);
                        break;
                    case E_STAT.DMG_SKILL:

                        break;
                    case E_STAT.CRITICAL_CHANCE:
                        targetUnit.SetExtraCriticalRate(_increaseRate * -1
                            );
                        break;
                    case E_STAT.ATTACK_SPEED:
                        targetUnit.SetExtraATKSpeed(_increaseRate * -1);
                        break;
                }
                targetUnit.GetBuffDebuffSlot().DeleteDebuff(this);
            }
            else
            {
                Monster targetMonster = _target.GetComponent<Monster>();
                if (targetMonster)
                {
                    switch (_statType)
                    {
                        case E_STAT.DEF:

                            break;
                        case E_STAT.REG:

                            break;
                        case E_STAT.MOVESPEED:

                            break;
                    }
                }
                targetMonster.GetBuffDebuffSlot().DeleteDebuff(this);
            }
            Destroy(this.gameObject);
         }
        else
        {
            Debug.LogError(this + "target is null");
        }
    }
}
