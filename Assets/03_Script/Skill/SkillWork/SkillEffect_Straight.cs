using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect_Straight : MonoBehaviour
{
    [SerializeField]
    float _duration;
    [SerializeField]
    int _damage;
    [SerializeField]
    float _maxDistance;
    [SerializeField]
    float _startDelay;
    public float GetStartDelay() { return _startDelay; }

    GameObject _target;
    //public void SetTarget(GameObject target) { _target = target; }
    TeamManager.E_TEAM _team;
    Property _property;
    public void InitializeEffect(GameObject target, TeamManager.E_TEAM team, Property property)
    {
        _target = target;
        _team = team;
        _property = property;
    }

    public void Effect()
    {
        Destroy(this.gameObject, _duration);
        if (_target)
        {
            //방향 감지
            Vector3 targetPos = _target.transform.position;
            targetPos.y = transform.position.y;
            Quaternion lookUpRotation = Quaternion.LookRotation(targetPos - transform.position);
            transform.rotation = lookUpRotation;

            //레이캐스트 쏘기
            targetPos = _target.transform.localPosition;
            Vector3 myPos = transform.localPosition;
            targetPos.y += 3f;
            myPos.y = targetPos.y;

            RaycastHit[] hit =
            Physics.RaycastAll(myPos, targetPos, _maxDistance,1 << LayerMask.NameToLayer("Monster"));
            Debug.DrawRay(myPos, targetPos * _maxDistance, Color.blue, 0.5f);
            if (hit.Length >0)
            {
                for(int i = 0; i < hit.Length; i++)
                {
                    int realDMG = (int)(_damage * (1 + TeamManager._instance.GetLevel(_team) * 0.1));
                    Monster target = hit[i].collider.gameObject.GetComponent<Monster>();
                    if(target)
                    {
                        if(_property.IsSameProperty(target.GetProperty()))
                        {
                            realDMG = (int)(realDMG * 1.5f);
                        }
                        target.Hit((int)realDMG, _property);
                    }
                }
            }

            hit = Physics.RaycastAll(transform.position, targetPos, 1 << LayerMask.NameToLayer("GoldMonster"));
            if (hit.Length > 0)
            {
                for (int i = 0; i < hit.Length; i++)
                {
                    int realDMG = (int)(_damage * (1 + TeamManager._instance.GetLevel(_team) * 0.1));
                    Monster target = hit[i].collider.gameObject.GetComponent<Monster>();
                    if (target)
                    {
                        if (_property.IsSameProperty(target.GetProperty()))
                        {
                            realDMG = (int)(realDMG * 1.5f);
                        }
                        target.Hit((int)realDMG, _property);
                    }
                }
            }
        }
    }

}
