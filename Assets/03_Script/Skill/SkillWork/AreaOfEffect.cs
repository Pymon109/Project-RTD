using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : MonoBehaviour
{
    [SerializeField]
    float _range;
    [SerializeField]
    float _duration;
    [SerializeField]
    float _cycle;
    [SerializeField]
    int _damage;

    [SerializeField]
    ParticleSystem _effectSite;

    TeamManager.E_TEAM _team;
    Property _property;

    List<GameObject> _targets = new List<GameObject>();

    public Unit _testUnit;

    public void InitializeAOE(float range, float duration, float cycle, int damage, TeamManager.E_TEAM team, Property property)
    {
        _range = range;
        _duration = duration;
        _cycle = cycle;
        _damage = damage;
        _team = team;
        _property = property;
        StartCoroutine(Effect());
        Destroy(this.gameObject, _duration);
    }
    public void InitializeAOE(TeamManager.E_TEAM team, Property property)
    {
        _team = team;
        _property = property;
        StartCoroutine(Effect());
        Destroy(this.gameObject, _duration);
    }

    IEnumerator Effect()
    {
        float currentTime = 0;
        while (currentTime <= _duration)
        {
            FindTargetInSite();
            if (_targets.Count > 0)
            {
                for (int i = 0; i < _targets.Count; i++)
                {
                    int realDMG = (int)(_damage * (1 + GameManager.instance.teamManager.GetLevel(_team) * 0.1));
                    Monster targetMonster = _targets[i].GetComponent<Monster>();
                    if (_property.IsSameProperty(targetMonster.GetProperty()))
                    {
                        realDMG = (int)(realDMG * 1.5f);
                    }
                    targetMonster.Hit((int)(realDMG), _property);
                }
            }
            yield return new WaitForSeconds(_cycle);
            currentTime += _cycle;
        }
        
    }

    public void FindTargetInSite()
    {
        _targets.Clear();
        int nLayer = 1 << LayerMask.NameToLayer("Monster");
        Collider[] collider = Physics.OverlapSphere(transform.position, _range * 10, nLayer);
        if (collider.Length > 0)
        {
            for (int i = 0; i < collider.Length; i++)
                _targets.Add(collider[i].gameObject);
        }
        nLayer = 1 << LayerMask.NameToLayer("GoldMonster");
        collider = Physics.OverlapSphere(transform.position, _range * 10, nLayer);
        if (collider.Length > 0)
        {
            for (int i = 0; i < collider.Length; i++)
                _targets.Add(collider[i].gameObject);
        }
    }


    private void Awake()
    {
        _effectSite.startSize *= _range;
    }

/*    private void Start()
    {
        _team = _testUnit.GetTeam();
        _property = _testUnit.GetProperty();
        StartCoroutine(Effect());
        Destroy(this.gameObject,_duration);
    }*/
}
