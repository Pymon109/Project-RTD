using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_ToolTipControl : MonoBehaviour
{
    static GUI_ToolTipControl _unique;
    public static GUI_ToolTipControl _instance { get { return _unique; } }

    [SerializeField]
    List<GameObject> _list_toolTips;

    public GameObject GetToolTipObject(E_GUI_TOOLTIP idx) { return _list_toolTips[(int)idx]; }
    public enum E_GUI_TOOLTIP
    {
        GAME = 0,
        SKILL,
        BUFF_DEBUFF,

        NONE = -1
    }

    E_GUI_TOOLTIP _currentToolTip = E_GUI_TOOLTIP.NONE;

    void UpdateToolTip()
    {
        switch(_currentToolTip)
        {
            case E_GUI_TOOLTIP.GAME:

                break;
            case E_GUI_TOOLTIP.SKILL:

                break;
            case E_GUI_TOOLTIP.BUFF_DEBUFF:

                break;
            case E_GUI_TOOLTIP.NONE:

                break;
        }
    }

    public void SetToolTip(E_GUI_TOOLTIP command)
    {
        switch (command)
        {
            case E_GUI_TOOLTIP.GAME:

                break;
            case E_GUI_TOOLTIP.SKILL:

                break;
            case E_GUI_TOOLTIP.BUFF_DEBUFF:

                break;
            case E_GUI_TOOLTIP.NONE:

                break;
        }
        _currentToolTip = command;
        ChangeToolTipScreen(command);
    }

    void ChangeToolTipScreen(E_GUI_TOOLTIP command)
    {
        for(int i = 0; i < _list_toolTips.Count; i++)
        {
            if ((int)command == i)
                _list_toolTips[i].SetActive(true);
            else
                _list_toolTips[i].SetActive(false);
        }
    }

    private void Awake()
    {
        _unique = this;
    }

    private void Update()
    {
        UpdateToolTip();
    }
}
