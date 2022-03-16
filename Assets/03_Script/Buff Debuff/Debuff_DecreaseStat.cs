using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff_DecreaseStat : Buff
{
    [SerializeField]
    int _decreaseAmount;

    [SerializeField]
    float _decreaseRate;

    enum E_STAT
    {
        DEF,
        REG,
        SPEED
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
        if(_target)
        {
            Monster targetMonster = _target.GetComponent<Monster>();
            if(targetMonster)
            {
                switch (_statType)
                {
                    case E_STAT.DEF:
                        targetMonster.SetExtraDEF(_decreaseAmount * -1);
                        break;
                    case E_STAT.REG:
                        targetMonster.SetExtraREG(_decreaseRate * -1);
                        break;
                    case E_STAT.SPEED:
                        MonsterAI targetMonsterAI = _target.GetComponent<MonsterAI>();
                        if (targetMonsterAI)
                            targetMonsterAI.SetSpeedDecrease(_decreaseRate);
                        break;
                }
            }
            if(gameObject.activeInHierarchy)
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
            Monster targetMonster = _target.GetComponent<Monster>();
            if(targetMonster)
            {
                switch (_statType)
                {
                    case E_STAT.DEF:
                        targetMonster.SetExtraDEF(_decreaseAmount);
                        break;
                    case E_STAT.REG:
                        targetMonster.SetExtraREG(_decreaseRate);
                        break;
                    case E_STAT.SPEED:
                        MonsterAI targetMonsterAI = _target.GetComponent<MonsterAI>();
                        if (targetMonsterAI)
                            targetMonsterAI.SetSpeedDecrease(_decreaseAmount * -1);
                        break;
                }
                targetMonster.GetBuffDebuffSlot().DeleteDebuff(this);
                Destroy(this.gameObject);
            }
            
        }
        else
        {
            Debug.LogError(this + "target is null");
        }
    }
 }
