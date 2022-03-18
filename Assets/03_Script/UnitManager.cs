using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField]
    SkillManager _skill_manager;

    List<int>[,] _unitOnTile = new List<int>[5, 6];
    public void AddUnitOnTile(int tileIdx, int unitSID)
    {
        _unitOnTile[unitSID / 100, unitSID % 100].Add(tileIdx);
    }
    public void DeleteUnitOnTile(int tileIdx, int unitSID)
    {
        _unitOnTile[unitSID / 100, unitSID % 100].Remove(tileIdx);
    }

    public enum E_Rank
    {
        NORMAL = 0,
        MAGIC,
        RARE,
        UNIQUE,
        EPIC,

        NONE = -1
    }

    public int FindSameUnit(int targetSID, int tileIdx)
    {
        int count = _unitOnTile[targetSID / 100, targetSID % 100].Count;
        if (count < 2)
            return -1;
        else
        {
            for (int i = 0; i < count; i++)
                if (_unitOnTile[targetSID / 100, targetSID % 100][i] != tileIdx)
                    return _unitOnTile[targetSID / 100, targetSID % 100][i];
        }
        return -1;
    }

    public int MaxCountOfSameUnit(E_Rank rank)
    {
        int count = 0;
        for (int i = 0; i < 6; i++)
        {
            int thisCount = _unitOnTile[(int)rank, i].Count;
            if (thisCount > count)
                count = thisCount;
        }
        return count;
    }

    public int CountOfDifferentUnit(E_Rank rank)
    {
        int count = 0;
        for (int i = 0; i < 6; i++)
            if (_unitOnTile[(int)rank, i].Count > 0)
                count++;
        return count;
    }

    public int CountOfAllUnit(E_Rank rank)
    {
        int count = 0;
        for (int i = 0; i < 6; i++)
            count += _unitOnTile[(int)rank, i].Count;
        return count;
    }

    public int CountOfThisUnit(int sid)
    {
        return _unitOnTile[sid / 100, sid % 100].Count;
    }

    private void Start()
    {
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 6; j++)
                _unitOnTile[i, j] = new List<int>();
    }

}
