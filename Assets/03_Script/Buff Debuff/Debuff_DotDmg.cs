using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff_DotDmg : Buff
{
    [SerializeField]
    int _damage;

    IEnumerator DotDamage()
    {
        //yield return new WaitForSeconds(_cycle);
        float currentTime = 0;
        while(currentTime <= _duration)
        {
            //데미지
            /*
            타겟
            버프를 부여한 주체
                > 유닛일 경우 해당 유닛의 진영 정보와 업그레드 레벨
            타겟 . 히트 (데미지 + (데제 * 0.1 * 업그레이드 레벨)
             */

            int realDMG = (int)(_damage * (1 + GameManager.instance.teamManager.GetLevel(_team) * 0.1));
            Monster targetMonster = _target.GetComponent<Monster>();
            if (_property.IsSameProperty(targetMonster.GetProperty()))
            {
                realDMG = (int)(realDMG * 1.5f);
            }
            targetMonster.Hit((int)(realDMG), _property);

            //Debug.Log(_name + " : dot damage.");
            yield return new WaitForSeconds(_cycle);
            currentTime += _cycle;
        }
        Destroy(this.gameObject);
    }

    override public void BuffOn()
    {
        if(gameObject.activeInHierarchy)
            StartCoroutine(DotDamage());
    }
}
