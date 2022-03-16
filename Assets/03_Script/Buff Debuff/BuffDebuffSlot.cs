using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuffSlot : MonoBehaviour
{
    [SerializeField]
    List<Buff> _buffs;

    int[] _buffCount = new int[30];
    public int[] GetBuffCounts() { return _buffCount; }
    public void AddBuffDebuff(Buff buff)
    {
        if(!buff.IsStackable())
        {
            for(int i = 0; i < _buffs.Count; i++)
            {
                if (buff.GetSID() == _buffs[i].GetSID())
                {
                    Destroy(buff.gameObject);
                    return;
                }
            }
        }
        _buffs.Add(buff);
        buff.transform.SetParent(transform);
        buff.SetTarget(transform.parent.gameObject);
        buff.GetComponent<Buff>().BuffOn();
        _buffCount[buff.GetSID()]++;
    }

    public void DeleteDebuff(Buff debuff)
    {
        _buffCount[debuff.GetSID()]--;
        _buffs.Remove(debuff);
    }

    public int CountOfBuff(bool countOfBuff)
    {
        int buffCount = 0;
        int debuffCount = 0;
        for(int i = 0; i < _buffs.Count; i++)
        {
            if (_buffs[i].IsDebuff())
                debuffCount++;
            else
                buffCount++;
        }
        if (countOfBuff)
            return buffCount;
        else
            return debuffCount;
    }

    public void SlotInit()
    {
        for (int i = 0; i < _buffs.Count; i++)
            if(_buffs[i])
                Destroy(_buffs[i].gameObject);
        _buffs.Clear();
        for (int i = 0; i < _buffCount.Length; i++)
            _buffCount[i] = 0;
    }

    private void Start()
    {
        for (int i = 0; i < _buffCount.Length; i++)
            _buffCount[i] = 0;
    }
}
