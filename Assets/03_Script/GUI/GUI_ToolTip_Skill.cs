using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_ToolTip_Skill : MonoBehaviour
{
    [SerializeField]
    Text _txt_title;
    [SerializeField]
    Text _txt_comment;
    [SerializeField]
    Text _txt_cooltime;
    [SerializeField]
    Image _img_skill_icon;

    public void SetSkillToolTip(Skill skill)
    {
        _txt_title.text = skill.GetName();
        _txt_comment.text = skill.GetComment();
        _txt_cooltime.text = "쿨타임 " + ((int)skill.GetCoolTime()).ToString() + "초";
        _img_skill_icon.sprite = Resources.Load<Sprite>("Sprite/SkillIcon/img_icon_" + skill.GetSkillID());
    }
}
