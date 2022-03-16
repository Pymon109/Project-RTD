using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    static RoundManager _unique;
    public static RoundManager _instance { get { return _unique; } }

    [SerializeField]
    GUI_RoundInfo _gui_roundInfo;

    [SerializeField]
    GUI_TopBar _gui_topBar;

    public List<Round> _list_round;
    int _currentRound = 0;
    public int GetCurrentRound()
    {
        return _currentRound;
    }

    public enum E_ROUND_STATE
    {
        DISABLE = 0,
        BIULDING,

        WAIT,
        PLAY,
        COMPLETE,
        END
    }
    E_ROUND_STATE _currentState = E_ROUND_STATE.DISABLE;
    public E_ROUND_STATE GetCurrentState() { return _currentState; }

    void UpdateRoundState()
    {
        switch (_currentState)
        {
            case E_ROUND_STATE.DISABLE:
                if (DataManager._instance.IsLoadData(DataManager.E_DATATYPE.ROUND))
                    SetRoundState(E_ROUND_STATE.BIULDING);
                break;
            case E_ROUND_STATE.BIULDING:
                break;
            case E_ROUND_STATE.WAIT:
                break;
            case E_ROUND_STATE.PLAY:
                if(_list_round[_currentRound].ProcessFindTarget("Monster"))
                {
                    SetRoundState(E_ROUND_STATE.COMPLETE);
                    Spawner._instance.SetState(Spawner.E_SPAWNER_STATE.SPAWN_END);
                }
                break;
            case E_ROUND_STATE.COMPLETE:
                break;
            case E_ROUND_STATE.END:
                break;
        }
    }

    public void SetRoundState(E_ROUND_STATE command)
    {
        switch (command)
        {
            case E_ROUND_STATE.DISABLE:
                break;
            case E_ROUND_STATE.BIULDING:
                BuildAllRound();
                break;
            case E_ROUND_STATE.WAIT:
                if (_list_round[_currentRound].IsBossRound())
                    _gui_roundInfo.SetState(GUI_RoundInfo.E_GUI_ROUND_INFO_STATE.WAIT_BR);
                else
                    _gui_roundInfo.SetState(GUI_RoundInfo.E_GUI_ROUND_INFO_STATE.WAIT_NR);
                StartCoroutine(CountDown());
                //SkillManager._instance.RoundSkillCastOn();
                break;
            case E_ROUND_STATE.PLAY:
                if (_list_round[_currentRound].IsBossRound())
                    _gui_roundInfo.SetState(GUI_RoundInfo.E_GUI_ROUND_INFO_STATE.PLAY_BR);
                else
                    _gui_roundInfo.SetState(GUI_RoundInfo.E_GUI_ROUND_INFO_STATE.PLAY_NR);
                StartCoroutine(_list_round[_currentRound].SpawnMonster());
                SkillManager._instance.RoundSkillCastOn();
                break;
            case E_ROUND_STATE.COMPLETE:
                _currentRound++;
                if (_currentRound >= _list_round.Count)
                {
                    GUIManager._instance.SetState(GUIManager.E_GUI_STATE.PLAY);
                    SetRoundState(E_ROUND_STATE.END);
                }
                else
                {
                    _gui_topBar.SetRoundText(_currentRound + 1);
                    SetRoundState(E_ROUND_STATE.WAIT);
                }
                break;
            case E_ROUND_STATE.END:
                GUIManager._instance.SetState(GUIManager.E_GUI_STATE.CLEAR);
                break;
        }
        _currentState = command;
    }

    IEnumerator CountDown()
    {
        while (_list_round[_currentRound].GetWaitingTime() > 0)
        {
            yield return new WaitForSeconds(1f);
            _list_round[_currentRound].ReduceWaitinTime();
            _gui_roundInfo.SetWaitingTime((int)_list_round[_currentRound].GetWaitingTime());
            if (_list_round[_currentRound].GetWaitingTime() == 2)
                Spawner._instance.SetState(Spawner.E_SPAWNER_STATE.SPAWNING);
                
        }
        SetRoundState(E_ROUND_STATE.PLAY);
    }

    void BuildAllRound()
    {
        RoundBuilder roundBuilder = (RoundBuilder)(DataManager._instance.GetBuilder(DataManager.E_DATATYPE.ROUND));

        for (int i = 1; i <= 40; i++)
        {
            string roundID = "round0";
            if (i < 10)
                roundID += "0";
            roundID += i.ToString();
            GameObject g_round = roundBuilder.BuildRound(roundID);
            _list_round.Add(g_round.GetComponent<Round>());
        }

        /*        GameObject g_round01 = roundBuilder.BuildRound("round001");
                _list_round.Add(g_round01.GetComponent<Round>());
                GameObject g_round40 = roundBuilder.BuildRound("round002");
                _list_round.Add(g_round40.GetComponent<Round>());*/

        SetRoundState(E_ROUND_STATE.WAIT);
    }

    private void Awake()
    {
        _unique = this;
    }

    private void Update()
    {
        UpdateRoundState();
    }
}
