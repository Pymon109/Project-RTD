using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_ToolTip_Buff : MonoBehaviour
{
    [SerializeField]
    Text _txt_title;
    [SerializeField]
    Text _txt_comment;
    [SerializeField]
    Image _img_buff_icon;

    public void SetBuffToolTip(Buff buff)
    {
        _txt_title.text = buff.GetName();
        _txt_comment.text = buff.GetComment();
        //Debug.Log(buff.GetBuffID());
        _img_buff_icon.sprite = Resources.Load<Sprite>("Sprite/BuffIcon/img_icon_" + buff.GetBuffID());
    }
}
