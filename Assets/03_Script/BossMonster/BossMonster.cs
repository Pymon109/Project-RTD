using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : Monster
{
    private void Awake()
    {
        _property = GetComponent<Property>();
    }

    private void Start()
    {
        _HP = _maxHP;
        //_isBoss = true;
    }

}
