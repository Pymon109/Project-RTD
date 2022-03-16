using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectObjectPool : ObjectPoolSubMaster
{
    public enum E_FX_TYPE
    {
        GUN = 0,
        SKILL,
        BUFF,
        TRACKING,
        TRACKING_EXPLODE
    }

    public ObjectPool GetPool(E_FX_TYPE type, int unitNum)
    {
        string key = "";
        switch (type)
        {
            case E_FX_TYPE.GUN:
                key = "effect_gun_ux0" + unitNum.ToString();
                break;
            case E_FX_TYPE.TRACKING:
                key = "effect_tracking_ux0" + unitNum.ToString();
                break;
            case E_FX_TYPE.TRACKING_EXPLODE:
                key = "effect_tracking_ux0" + unitNum.ToString() + "_explosion";
                break;
        }
        return m_pools[key];
    }
    public ObjectPool GetPool(E_FX_TYPE type, string skillID)
    {
        string key = "";
        switch (type)
        {
            case E_FX_TYPE.SKILL:
                key = "effect_" + skillID;
                break;
            case E_FX_TYPE.BUFF:
                key = "effect_" + skillID + "_buff";
                break;
        }
        if(m_pools.ContainsKey(key))
            return m_pools[key];
        return null;
    }

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            ObjectPool pool = transform.GetChild(i).GetComponent<ObjectPool>();
            m_pools.Add(pool.gameObject.name, pool);
            m_keys.Add(pool.prefabName);
            pool.InitQueu();
        }
    }
}
