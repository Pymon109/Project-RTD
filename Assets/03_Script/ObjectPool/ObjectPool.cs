using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject m_poolingObject;
    public string prefabName { get { return m_poolingObject.name; } }
    [SerializeField] int m_iInitCount;
    public GameObject poolingObject 
    { 
        set 
        { 
            m_poolingObject = value;
            m_poolingObject.transform.SetParent(ObjectPoolManager.instance.trsTemplete);
            m_poolingObject.SetActive(false);
        } 
    }
    Queue<GameObject> m_poolingObjectQueue = new Queue<GameObject>();



    public void InitQueu()
    {
        for (int i = 0; i < m_iInitCount; i++)
            m_poolingObjectQueue.Enqueue(CreateNewObject());
    }
    public void InitQueu(Vector3 initPos)
    {
        for (int i = 0; i < m_iInitCount; i++)
            m_poolingObjectQueue.Enqueue(CreateNewObject(initPos));
    }
    GameObject CreateNewObject()
    {
        var newObj = Instantiate(m_poolingObject);
        newObj.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }
    GameObject CreateNewObject(Vector3 initPos)
    {
        var newObj = Instantiate(m_poolingObject);
        newObj.SetActive(false);
        newObj.transform.SetParent(transform);
        newObj.transform.position = initPos;
        return newObj;
    }
    public GameObject GetObject(Transform parent)
    {
        if(m_poolingObjectQueue.Count > 0)
        {
            var obj = m_poolingObjectQueue.Dequeue();
            obj.transform.SetParent(parent);
            obj.SetActive(true);
            return obj;
        }
        else
        {
            var obj = CreateNewObject();
            obj.transform.SetParent(parent);
            obj.SetActive(true);
            return obj;
        }
    }
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        m_poolingObjectQueue.Enqueue(obj);
    }
}
