using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWork : MonoBehaviour
{

    public enum E_TARGET_RANGE
    {
        SINGLE = 0,
        MULTI,

        NONE = -1
    }
    [SerializeField]
    E_TARGET_RANGE _target_range;

    public enum E_TARGET_TYPE
    {
        MONSTER = 0,
        FRIENDLY,
        MY,
        MYMONSTER,
        PLAYER,

        NONE = -1
    }
    [SerializeField]
    protected E_TARGET_TYPE _target_type;
    public E_TARGET_TYPE GetTargetType() { return _target_type; }

    [SerializeField]
    protected float _site;

    public enum E_SITE_TARGET_TYPE
    {
        ATTACK_TARGET = 0,
        MY,

        NONE = -1
    }
    [SerializeField]
    E_SITE_TARGET_TYPE _site_target;

    public enum E_WORK_TYPE
    {
        DAMAGE = 0,
        BUFF,
        HP,
        GOLD, 
        COOLTIME,
        AOE,
        STRAIGHT,

        NONE = -1
    }
    [SerializeField]
    protected E_WORK_TYPE _work_type;

    [SerializeField]
    protected Unit _parent_unit;

    public void SetSkillWork(E_TARGET_RANGE targetRange, E_TARGET_TYPE targetType, float site, 
        E_SITE_TARGET_TYPE siteTargetType, E_WORK_TYPE workType, Unit parentUnit)
    {
        _target_range = targetRange;
        _target_type = targetType;
        _site = site;
        _site_target = siteTargetType;
        _work_type = workType;
        _parent_unit = parentUnit;
    }

    protected TeamManager.E_TEAM _team;
    protected Property _property;

    protected GameObject _target;
    protected List<GameObject> _targets = new List<GameObject>();
    public void SetTarget(GameObject target = null)
    {
        switch (_target_type)
        {
            case E_TARGET_TYPE.MONSTER:
                _target = target;
                if (_site > 0)
                    FindTargetInSite();
                break;
            case E_TARGET_TYPE.FRIENDLY:
                
                break;
            case E_TARGET_TYPE.MY:
                _target = _parent_unit.gameObject;
                break;
            case E_TARGET_TYPE.MYMONSTER:

                break;
            case E_TARGET_TYPE.PLAYER:
                _target = Player._instance.gameObject;
                break;
            case E_TARGET_TYPE.NONE:

                break;
        }

/*        _target = target;
        if(_site > 0)
            FindTargetInSite();*/
    }
    public void FindTargetInSite()
    {
        if (_target)
        {
            _targets.Clear();
            int nLayer = 1 << LayerMask.NameToLayer("Monster");
            Collider[] collider = Physics.OverlapSphere(_target.transform.position, _site * 10, nLayer);
            if (collider.Length > 0)
            {
                for (int i = 0; i < collider.Length; i++)
                    _targets.Add(collider[i].gameObject);
            }
            nLayer = 1 << LayerMask.NameToLayer("GoldMonster");
            collider = Physics.OverlapSphere(transform.position, _site * 10, nLayer);
            if (collider.Length > 0)
            {
                for (int i = 0; i < collider.Length; i++)
                    _targets.Add(collider[i].gameObject);
            }
        }
        else
            _targets.Clear();
    }

    virtual public void Casting()
    {
        Debug.Log("skill work cast on");
    }
}
