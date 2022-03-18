using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWork_Gold : SkillWork
{
    [SerializeField]
    int _gold;

    override public void Casting()
    {
        GameManager.instance.player.AddGold(_gold);
    }

    private void Awake()
    {
        _work_type = E_WORK_TYPE.GOLD;
    }
}
