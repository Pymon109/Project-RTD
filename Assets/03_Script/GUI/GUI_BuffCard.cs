using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_BuffCard : MonoBehaviour
{
    [SerializeField]
    Image _img_icon_buff;
    [SerializeField]
    GUI_BuffIcon _gui_buffIcon;

    [SerializeField]
    Text _txt_count;

    public void SetBuffCard(string buffID, int count)
    {
        _img_icon_buff.sprite = Resources.Load<Sprite>("Sprite/BuffIcon/img_icon_" + buffID);
        _txt_count.text = count.ToString();
        Buff buff = Resources.Load<Buff>("Prefabs/BuffDebuff/s_" + buffID);
        _gui_buffIcon.SetBuffl(buff);
    }
}
