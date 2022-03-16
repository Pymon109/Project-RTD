using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField]
    float _destroyTime;
    EffectObjectPool.E_EFFECT_TYPE m_effectType;
    SkillEffectObjectPool.E_FX_TYPE m_skillEffectType;
    ObjectPool m_pool;

    public void EffectStart(ObjectPool pool)
    {
        m_pool = pool;
        Invoke("ReturnEffect_pool", _destroyTime);
    }
    void ReturnEffect_pool()
    {
        //Destroy(gameObject);
        m_pool.ReturnObject(gameObject);
    }

   /* public void EffectStart(EffectObjectPool.E_EFFECT_TYPE type)
    {
        m_effectType = type;
        Invoke("ReturnEffect", _destroyTime);
    }
    void ReturnEffect()
    {
        //Destroy(gameObject);
        EffectObjectPool effectPool = (EffectObjectPool)ObjectPoolManager.instance.m_pools[(int)ObjectPoolManager.E_POOL_TYPE.EFFECT];
        effectPool.GetPool(m_effectType).ReturnObject(gameObject);
    }

    public void EffectStart(SkillEffectObjectPool.E_FX_TYPE type)
    {
        m_skillEffectType = type;
        Invoke("ReturnEffect_skill", _destroyTime);
    }
    void ReturnEffect_skill()
    {
        SkillEffectObjectPool skillEffectPool = (SkillEffectObjectPool)ObjectPoolManager.instance.m_pools[(int)ObjectPoolManager.E_POOL_TYPE.SKILL_EFFECT];
        //skillEffectPool.GetPool(SkillEffectObjectPool.E_FX_TYPE.SKILL, skillID).ReturnObject(gameObject);
    }*/

    public void DestroyEffectStart()
    {
        Destroy(gameObject, _destroyTime);
    }

    private void Start()
    {
        //Invoke("DestoryEffect", _destroyTime);
    }
}
