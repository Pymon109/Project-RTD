using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    GameObject _path;
    public void SetPath(GameObject path) 
    {
        _list_path.Clear();
        _path = path;

        for (int i = 0; i < _path.transform.childCount; i++)
        {
            GameObject tmp = _path.transform.GetChild(i).gameObject;
            _list_path.Add(tmp);
        }
        _targetIdx = 1;
        _target = _list_path[_targetIdx];
    }

    [SerializeField] float _speed;
    public float GetSpeed() { return _speed; }
    float _decreaseSpeed = 0;
    public void SetSpeedDecrease(float decrease) 
    { 
        _decreaseSpeed += decrease;
        if (_decreaseSpeed < 0)
            _decreaseSpeed = 0;
    }
    float GetRealSpeed()
    {
        float realDecreaseSpeed = 1 - _decreaseSpeed;
        if (realDecreaseSpeed < 0)
            realDecreaseSpeed = 0;
        return _speed * realDecreaseSpeed;
    }

    Monster _monster_this;

    List<GameObject> _list_path = new List<GameObject>();
    int _targetIdx = 1;
    public int GetTargetPathIdx() { return _targetIdx; }
    public void SetTargetPathIdx(int idx) 
    { 
        _targetIdx = idx;
        _target = _list_path[_targetIdx];
    }
    GameObject _target = null;

    private void Move()
    {
        if(_target)
        {
            if ((transform.position - _target.transform.position).magnitude < 1.5f)
            {
                if (++_targetIdx >= _list_path.Count)
                {
                    //최종 목적지까지 도달
                    _monster_this.Attack();
                    //폭발 이펙트
                    GameManager.instance.effectManager.CreateGoalInEffect(transform.position);
                    _monster_this.Death();
                    return;
                }
                _target = _list_path[_targetIdx];
                Quaternion lookUpRotation = Quaternion.LookRotation(_target.transform.position - transform.position);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookUpRotation, 90f);

            }
            Vector3 vPos = this.transform.position;
            Vector3 vTargetPos = _target.transform.position;
            Vector3 vDist = vTargetPos - vPos;  //타겟 - 나 잇는 벡터
            Vector3 vDir = vDist.normalized;    //벡터의 정규화 (방향 벡터로 만들어줌)

            transform.position += vDir * GetRealSpeed() * Time.deltaTime;
        }
    }

    private void Awake()
    {
        _monster_this = GetComponent<Monster>();
    }

    private void Update()
    {
        Move();
    }

}
