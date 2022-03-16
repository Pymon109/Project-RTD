using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_TopBar : MonoBehaviour
{
    [SerializeField]
    Text _txt_HP;

    [SerializeField]
    Text _txt_round;

    [SerializeField]
    Text _txt_gold;

    public void SetHPTextP(int hp)
    {
        _txt_HP.text = hp.ToString();
    }

    public void SetRoundText(int round)
    {
        _txt_round.text = "Round " + round.ToString();
    }

    public void SetGoldText(int gold)
    {
        _txt_gold.text = gold.ToString();
    }
}
