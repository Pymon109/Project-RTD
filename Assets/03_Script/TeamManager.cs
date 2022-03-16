using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    static TeamManager _unique;
    public static TeamManager _instance { get { return _unique; } }

    [SerializeField]
    List<GUI_TeamCard> _list_teamCard;

    public enum E_TEAM
    {
        UNION = 0,
        DEMIC,
        AXIS,

        NONE = -1
    }
    int[] _level = new int[3];
    int[,] _rankCount = new int[3, 5];

    [SerializeField]
    AudioSource _audio_upgrade;

    public int GetDemendGold(E_TEAM team)
    {
        //골드 공식 바꿔야 함
        return 10 + (_level[(int)team] * 2);
    }

    void IncreaseLevel(E_TEAM team)
    {
        if(Player._instance.SpendGold(GetDemendGold(team)))
        {
            _list_teamCard[(int)team].SetLevel(++_level[(int)team]);
            _list_teamCard[(int)team].SetGold(GetDemendGold(team));
            _audio_upgrade.Play();
            List<CountCondition> conditionList = MissionManager._instance.GetCondition(CountCondition.E_COUNT_CONDITION_TYPE.UPGRADE);
           // Debug.Log("conditionList's count = " + conditionList.Count);
            for (int i = 0; i < conditionList.Count; i++)
            {
                conditionList[i].AddCount(1);
            }
        }
    }
    public void OnButtonUnion() { IncreaseLevel(E_TEAM.UNION); }
    public void OnButtonDemic() { IncreaseLevel(E_TEAM.DEMIC); }
    public void OnButtonAxis() { IncreaseLevel(E_TEAM.AXIS); }
    public int GetLevel(E_TEAM team)
    {
        return _level[(int)team];
    }
    public void AddUnit(E_TEAM team, UnitManager.E_Rank rank)
    {
        _rankCount[(int)team, (int)rank]++;
        //_list_teamCard[(int)team].SetUnitCount(rank, ++_rankCount[(int)team, (int)rank]);
    }
    public void DeleteUnit(E_TEAM team, UnitManager.E_Rank rank)
    {
        _rankCount[(int)team, (int)rank]--;
        //_list_teamCard[(int)team].SetUnitCount(rank, --_rankCount[(int)team, (int)rank]);
    }
    public int GetUnitCount(E_TEAM team, UnitManager.E_Rank rank)
    {
        return _rankCount[(int)team, (int)rank];
    }

    public void UpdateTeamCardGUI()
    {
        for(int team = 0; team < 3; team++)
        {
            int[] count = new int[5];
            for (int i = 0; i < 5; i++)
                count[i] = _rankCount[team, i];
            _list_teamCard[team].InitUnitCount(count);
        }
    }

    private void Awake()
    {
        _unique = this;
    }

    private void Start()
    {
        _audio_upgrade.volume = 0.25f;
        _audio_upgrade.Stop();
    }
}
