using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_WaitingRound : MonoBehaviour
{
    [SerializeField]
    Text _txt_waitingTime;

    public void SetWaitingTime(int time)
    {
        _txt_waitingTime.text = time.ToString() + "s";
    }
}
