using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWork_AOE : SkillWork
{
    [SerializeField]
    AreaOfEffect _AOE;
    public void SetAOE(AreaOfEffect aoe) { _AOE = aoe; }

    override public void Casting()
    {
        if(_target)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/AOE/"+ _AOE.gameObject.name);
            GameObject instance = Instantiate(prefab);
            instance.transform.position = _target.transform.position;
            AreaOfEffect AOE = instance.GetComponent<AreaOfEffect>();
            AOE.InitializeAOE(_team, _property);
        }
    }

    private void Awake()
    {
        _work_type = E_WORK_TYPE.AOE;
    }

    private void Start()
    {
        _team = _parent_unit.GetTeam();
        _property = _parent_unit.GetProperty();
    }

    private void Update()
    {
        _target = _parent_unit.GetTarget();
    }
}
