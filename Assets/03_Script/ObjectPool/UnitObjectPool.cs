using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitObjectPool : ObjectPoolSubMaster
{
    public ObjectPool GetPool(int sid)
    {
        string unitID = "u";
        if (sid < 10)
            unitID += "00";
        else if (sid < 100)
            unitID += "0";
        unitID += sid.ToString();
        return m_pools[unitID];
    }

    private void Start()
    {
        for(int i = 0; i < transform.childCount; i ++)
        {
            ObjectPool pool = transform.GetChild(i).GetComponent<ObjectPool>();
            m_pools.Add(pool.gameObject.name, pool);
            //m_keys.Add(pool.prefabName);
        }
    }
}
