using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    static ObjectPoolManager unique;
    public static ObjectPoolManager instance { get { return unique; } }
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

    private void Awake()
    {
        unique = this;
    }
}
