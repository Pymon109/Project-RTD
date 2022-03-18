using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingBall : MonoBehaviour
{
    [SerializeField]
    GameObject _prefabExplosionEffect;

    int m_iUnitNum;
    int _damage;
    Property _property;
    Monster _target;

    bool _isReady = false;
    public void Init(int unitNum, int damage, Property property, Monster monster)
    {
        m_iUnitNum = unitNum;
        _damage = damage;
        _property = property;
        _target = monster;
        _isReady = true;
    }

    float _moveSpeed = 250;
    float _rotationSpeed = 500;

    void Tracking()
    {
        if(_target)
        {
            //타겟쪽을 바라보게 돌리기
            Quaternion lookUpRotation = Quaternion.LookRotation(_target.transform.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookUpRotation, Time.deltaTime * _rotationSpeed);

            //앞으로
            //transform.position += Vector3.forward * Time.deltaTime * _moveSpeed;
            Vector3 vPos = this.transform.position;
            Vector3 vTargetPos = _target.transform.position;
            Vector3 vDist = vTargetPos - vPos;  //타겟 - 나 잇는 벡터
            Vector3 vDir = vDist.normalized;    //벡터의 정규화 (방향 벡터로 만들어줌)
            float fDist = vDist.magnitude;

            if (fDist > Time.deltaTime * _moveSpeed)
            {
                transform.position += vDir * _moveSpeed * Time.deltaTime;
            }
            else
            {
                //공격
                Vector3 explosionPos = transform.position;
                explosionPos.y += 5;
                //EffectManager._instance.CreateEffect(explosionPos, _prefabExplosionEffect.name);
                GameManager.instance.effectManager.CreateExplodeEffect(explosionPos, m_iUnitNum);
                _target.Hit(_damage, _property);
                gameObject.SetActive(false);
            }

        }
        else
            gameObject.SetActive(false);
    }

    private void Update()
    {
        if(_isReady)
            Tracking();
    }
}
