using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMonsterManager : MonoBehaviour
{
    int _nextLevel = 1;
    public int GetNextLevel() { return _nextLevel; }

    [SerializeField]
    GUI_GoldMonsterButton _gui_goldMonsterButton;

    public void IncreaseLevel() 
    { 
        _gui_goldMonsterButton.SetGoldMonsterLevelText(++_nextLevel);
    }
}
