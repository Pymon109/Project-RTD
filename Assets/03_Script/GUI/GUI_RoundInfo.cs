using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_RoundInfo : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _listRoundInfo;
    public enum E_GUI_ROUND_INFO_STATE
    {
        WAIT_NR = 0,
        PLAY_NR,
        WAIT_BR,
        PLAY_BR
    }
    E_GUI_ROUND_INFO_STATE _currentState = E_GUI_ROUND_INFO_STATE.WAIT_NR;

    void UpdateState()
    {
        switch (_currentState)
        {
            case E_GUI_ROUND_INFO_STATE.WAIT_NR:
                break;
            case E_GUI_ROUND_INFO_STATE.PLAY_NR:
                break;
            case E_GUI_ROUND_INFO_STATE.WAIT_BR:
                break;
            case E_GUI_ROUND_INFO_STATE.PLAY_BR:
                break;
        }
    }

    public void SetState(E_GUI_ROUND_INFO_STATE command)
    {
        switch(command)
        {
            case E_GUI_ROUND_INFO_STATE.WAIT_NR:
                break;
            case E_GUI_ROUND_INFO_STATE.PLAY_NR:
                break;
            case E_GUI_ROUND_INFO_STATE.WAIT_BR:
                break;
            case E_GUI_ROUND_INFO_STATE.PLAY_BR:
                StartCoroutine(_listRoundInfo[3].GetComponent<GUI_BossRound>().ActiveWarningSign());
                break;
        }
        _currentState = command;
        ShowRoundGUIScence(command);
    }

    void ShowRoundGUIScence(E_GUI_ROUND_INFO_STATE state)
    {
        for(int i = 0; i<_listRoundInfo.Count; i++)
        {
            if (i == (int)state)
                _listRoundInfo[i].SetActive(true);
            else
                _listRoundInfo[i].SetActive(false);
        }
    }

    public void SetWaitingTime(int time)
    {
        GUI_WaitingRound waitingRound = _listRoundInfo[(int)_currentState].GetComponent<GUI_WaitingRound>();
        if (waitingRound)
            waitingRound.SetWaitingTime(time);
    }

    private void Update()
    {
        UpdateState();
    }
}
