using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_ToolTip_System : MonoBehaviour
{
    [SerializeField]
    Text _txt_title;
    [SerializeField]
    Text _txt_comment;
    [SerializeField]
    Text _txt_key;
    public void SetSystemTooltip(string title, string comment, string key)
    {
        _txt_title.text = title;
        _txt_comment.text = comment;
        _txt_key.text = "[" + key + "]";
    }
}
