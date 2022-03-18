using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObjectPool : ObjectPoolSubMaster
{
    public enum E_MONSTER_TYPE
    {
        SLIME_FIRE = 0,
        SLIME_FOREST,
        SLIME_WARTER,
        BOSS_MIMIC,
        BOSS_ROBOT,
        BOSS_VIRUS,
        GOLD_MONSTER,
        GUI_HA_BAR,

        max_count
    }
    public ObjectPool GetPool(E_MONSTER_TYPE type) { return m_pools[GetKey((int)type)]; }

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            ObjectPool pool = transform.GetChild(i).GetComponent<ObjectPool>();
            m_pools.Add(pool.gameObject.name, pool);
            m_keys.Add(pool.gameObject.name);
            pool.InitQueu(GameManager.instance.spawner.transform.position);
        }
        m_bPoolReady = true;
    }
}
