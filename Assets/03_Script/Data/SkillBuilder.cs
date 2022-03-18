using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBuilder : Builder
{
    public GameObject BuildSkill(string skillID, Unit parentUnit)
    {
        //데이터 가져오기
        //SkillData.s_skillInfo skillInfo = GameManager.instance.dataManager.GetSkillInfo(skillID);
        SkillData.s_skillInfo skillInfo = GameManager.instance.fileManager.GetSkillInfo(skillID);

        //프리팹 가져오기
        GameObject prefabFram = Resources.Load<GameObject>("Prefabs/Skill/SkillFrame");
        GameObject newFram = Instantiate(prefabFram);

        //데이터 설정
        Skill skill = newFram.GetComponent<Skill>();
        skill.SetSkillData(skillInfo._sid, skillID, skillInfo._name, skillInfo._cast_chance, skillInfo._cooltime, skillInfo._casting_time, skillInfo._comment);

        //트리거 생성
        string prefabName = "trigger_";
        switch (skillInfo._trigger_condition)
        {
            case "SELL":
                prefabName += "sell";
                break;
            case "ATTACK":
                prefabName += "attack";
                break;
            case "ROUNDSTART":
                prefabName += "round";
                break;
            case "TOWER":
                prefabName += "hasTower";
                break;
            case "CONTINUE":
                prefabName += "continue";
                break;
            case "DEBUFF":
                prefabName += "debuff";
                break;
            case "NONE":
                prefabName += "none";
                break;
            default:
                prefabName += "none";
                break;
        }
        GameObject prefabTrigger = Resources.Load<GameObject>("Prefabs/Skill/Trigger/" + prefabName);
        GameObject newTrigger = Instantiate(prefabTrigger, transform.position, transform.rotation);
        newTrigger.transform.SetParent(newFram.transform);
        SkillTrigger trigger = newTrigger.GetComponent<SkillTrigger>();
        skill.SetTrigger(trigger);

        //이펙트 생성
        GameObject g_effect1, g_effect2 = null, g_effect3 = null;
        //SkillEffectBuilder effectBuilder = (SkillEffectBuilder)DataManager._instance.GetBuilder(DataManager.E_DATATYPE.SKILLEFFECTDATA);
        SkillEffectBuilder effectBuilder = (SkillEffectBuilder)GameManager.instance.dataManager.GetBuilder(DataManager.E_DATATYPE.SKILLEFFECTDATA);
        
        g_effect1 = effectBuilder.BuildSkillEffect(skillInfo._effect_1, parentUnit);
        skill.AddSkillWork(g_effect1.GetComponent<SkillWork>());
        g_effect1.transform.SetParent(newFram.transform);

        if (skillInfo._effect_2 != "")
        {
            g_effect2 = effectBuilder.BuildSkillEffect(skillInfo._effect_2, parentUnit);
            skill.AddSkillWork(g_effect2.GetComponent<SkillWork>());
            g_effect2.transform.SetParent(newFram.transform);
        }
        if (skillInfo._effect_3 != "")
        {
            g_effect3 = effectBuilder.BuildSkillEffect(skillInfo._effect_3, parentUnit);
            skill.AddSkillWork(g_effect3.GetComponent<SkillWork>());
            g_effect3.transform.SetParent(newFram.transform);
        }

        newFram.name = skillInfo._name;
        return newFram;
    }
}
