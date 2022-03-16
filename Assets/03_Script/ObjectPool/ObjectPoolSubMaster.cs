using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolSubMaster : MonoBehaviour
{
    protected Dictionary<string, ObjectPool> m_pools = new Dictionary<string, ObjectPool>();
    [SerializeField] protected List<string> m_keys = new List<string>();
    public string GetKey(int index) { return m_keys[index]; }
    public ObjectPool GetPool(string key) { return m_pools[key]; }
}