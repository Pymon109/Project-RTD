using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster_GoldDrop : Monster
{
    [SerializeField]
    float _goldrate;

    override public void Hit(int dmg, Property property)
    {
        int next = Random.RandomRange(0, 100);
        if(next <= 100 * _goldrate)
        {
            GoldDrop();
        }

        int realDMG = (int)(dmg * (1 - GetDEFChance() - GetRealREG(property)));
        _HP -= realDMG;

        GameObject prefap = Resources.Load<GameObject>("Prefabs/Effect/" + _prefap_dmgEffect.name);
        GameObject newobj = Instantiate(prefap, _pos_dmgEffect.position, prefap.transform.rotation);
        newobj.GetComponent<Effect_dmg>().SetDmgText(realDMG);

        if (_HP <= 0)
        {
            //Destroy(_guiStatusBar.gameObject);
            Destroy(gameObject);
        }
        else
            _guiStatusBar.SetState(_HP, _maxHP);
    }

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
