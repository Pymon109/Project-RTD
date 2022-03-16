using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_GoldMonsterButton : MonoBehaviour
{
    [SerializeField]
    Text _txt_level;
    [SerializeField]
    GameObject _buttonEnable;
    [SerializeField]
    Text _txt_countDown;
    [SerializeField]
    Button _button;

    public void SetGoldMonsterLevelText(int level)
    {
        _txt_level.text = "Lv." + level.ToString();
    }
    public void SetCountDown(int sec)
    {
        _txt_countDown.text = sec.ToString() + "초";
    }
    public void ButtonSetActive(bool command)
    {
        _button.enabled = command;
        _buttonEnable.SetActive(!command);
    }
}
