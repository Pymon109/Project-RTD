using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public Transform trsTemplete;
    public Transform trsDynamicObjects;

    public enum E_POOL_TYPE
    {
        UNIT = 0,
        MONSTER,
        EFFECT,
        SKILL_EFFECT,

        MAXCOUNT
    }
    public List<ObjectPoolSubMaster> m_pools;
    public bool IsPoolReady()
    {
        bool ready = true;
        for (int i = 0; i < m_pools.Count; i++)
            ready &= m_pools[i].bPoolReady;
        return ready;
    }
}
