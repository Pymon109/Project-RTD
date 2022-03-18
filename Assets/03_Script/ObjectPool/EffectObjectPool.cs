using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObjectPool : ObjectPoolSubMaster
{
    public enum  E_EFFECT_TYPE
    {
        TEXT = 0,
        GOAL_IN,
        EXPLODE,
        GOLD,

        max_count
    }
    public ObjectPool GetPool(E_EFFECT_TYPE type) { return m_pools[GetKey((int)type)]; }

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            ObjectPool pool = transform.GetChild(i).GetComponent<ObjectPool>();
            m_pools.Add(pool.gameObject.name, pool);
            m_keys.Add(pool.gameObject.name);
            pool.InitQueu();
        }
        m_bPoolReady = true;
    }
}
