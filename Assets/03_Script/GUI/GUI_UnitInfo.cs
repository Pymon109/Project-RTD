using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_UnitInfo : MonoBehaviour
{
    [SerializeField]
    Image _img_portrait;
    [SerializeField]
    Text _txt_name;
    [SerializeField]
    Image _img_team;
    [SerializeField]
    Text _txt_team;
    [SerializeField]
    Text _txt_atk;
    [SerializeField]
    Text _txt_atk_speed;
    [SerializeField]
    Image _img_skill01;
    [SerializeField]
    GUI_SkillIcon _gui_skillIcon_01;
    [SerializeField]
    Image _img_skill02;
    [SerializeField]
    GUI_SkillIcon _gui_skillIcon_02;
    [SerializeField]
    Transform _trs_buff_debuff_slot;

    Unit _targetUnit;

    public void SetUnitInfo(Unit targetUnit)
    {
        _targetUnit = targetUnit;
        int SID = targetUnit.GetSID();
        //초상화 불러오기
        Sprite portraitIMG = Resources.Load<Sprite>("Sprite/UI/Portrait/_img_portrait_" + SID.ToString());
        if (portraitIMG)
            _img_portrait.sprite = portraitIMG;

        _txt_name.text = targetUnit.GetName();

        //속성 이미지 불러오기
        Debug.Log("<GUI_CouponUnitCard_Element> SetCard() need to setting attribute image.");

        TeamManager.E_TEAM team = targetUnit.GetTeam();
        string s_team = "";
        switch (team)
        {
            case TeamManager.E_TEAM.UNION:
                s_team = "유니온";
                break;
            case TeamManager.E_TEAM.DEMIC:
                s_team = "데믹";
                break;
            case TeamManager.E_TEAM.AXIS:
                s_team = "액시스";
                break;
        }
        Sprite teamIMG = Resources.Load<Sprite>("Sprite/UI/TeamIcon/img_" + s_team + "_icon");
        if (teamIMG)
            _img_team.sprite = teamIMG;

        _txt_atk.text = targetUnit.GetATK().ToString();
        _txt_atk_speed.text = targetUnit.GetATKspeed().ToString();

        //스킬 이미지 불러오기
        Skill[] skills = targetUnit.GetSkills();
        string s_skill01 = "skill_none", s_skill02 = "skill_none";
        _gui_skillIcon_01.InitSkill();
        _gui_skillIcon_02.InitSkill();

        if (skills[0])
        {
            s_skill01 = skills[0].GetSkillID();
            _gui_skillIcon_01.SetSkill(skills[0]);
        }
        if (skills[1])
        {
            s_skill02 = skills[1].GetSkillID();
            _gui_skillIcon_02.SetSkill(skills[1]);
        }

        Sprite skill01IMG = Resources.Load<Sprite>("Sprite/SkillIcon/img_icon_"+ s_skill01);
        _img_skill01.sprite = skill01IMG;
        Sprite skill02IMG = Resources.Load<Sprite>("Sprite/SkillIcon/img_icon_" + s_skill02);
        _img_skill02.sprite = skill02IMG;

        //버프 디버프 슬롯
        UpdateBuffDebuffInfo();
    }
    void UpdateBuffDebuffInfo()
    {
        if(_targetUnit)
        {
            for (int i = 0; i < _trs_buff_debuff_slot.childCount; i++)
            {
                Destroy(_trs_buff_debuff_slot.GetChild(i).gameObject);
            }
            BuffDebuffSlot buffDebuffSlot = _targetUnit.GetBuffDebuffSlot();
            int[] buffCounts = buffDebuffSlot.GetBuffCounts();
            for (int i = 0; i < buffCounts.Length; i++)
            {
                if (buffCounts[i] > 0)
                {
                    GameObject Prefab = Resources.Load<GameObject>("Prefabs/GUI_buffCard");
                    GameObject newObj = Instantiate(Prefab, _trs_buff_debuff_slot);
                    string buffID = "buff0";
                    if (i < 10)
                        buffID += "0" + i.ToString();
                    else
                        buffID += i.ToString();
                    newObj.GetComponent<GUI_BuffCard>().SetBuffCard(buffID, buffCounts[i]);
                }
            }
        }
    }

    private void Update()
    {
        UpdateBuffDebuffInfo();
    }
}
