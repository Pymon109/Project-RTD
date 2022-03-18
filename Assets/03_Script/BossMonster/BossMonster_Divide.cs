using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster_Divide : Monster
{
    [SerializeField]
    GameObject _divideMonster;

    override public void Hit(int dmg, Property property)
    {
        int realDMG = (int)(dmg * (1 - GetDEFChance() - GetRealREG(property)));
        _HP -= realDMG;

        GameObject prefap = Resources.Load<GameObject>("Prefabs/Effect/" + _prefap_dmgEffect.name);
        GameObject newobj = Instantiate(prefap, _pos_dmgEffect.position, prefap.transform.rotation);
        newobj.GetComponent<Effect_dmg>().SetDmgText(realDMG);

        if (_HP <= 0)
        {
            for(int i = 0; i < 2; i++)
            {
                GameObject newDivideMonster = GameManager.instance.spawner.SpawnMonster(_divideMonster, _level);
                newDivideMonster.transform.position = transform.position;
                newDivideMonster.transform.rotation = transform.rotation;
                newDivideMonster.GetComponent<MonsterAI>().SetTargetPathIdx(GetComponent<MonsterAI>().GetTargetPathIdx());
            }
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
