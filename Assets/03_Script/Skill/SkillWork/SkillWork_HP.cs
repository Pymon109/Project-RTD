using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWork_HP : SkillWork
{
    [SerializeField]
    int _hp;

    override public void Casting()
    {
        GameManager.instance.player.AddHP(_hp);
    }

    private void Awake()
    {
        _work_type = E_WORK_TYPE.HP;
    }

}
