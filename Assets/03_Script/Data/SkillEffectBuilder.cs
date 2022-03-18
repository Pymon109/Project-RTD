using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectBuilder : Builder
{
    public GameObject BuildSkillEffect(string skillEffectID, Unit parentUnit)
    {
        //데이터 가져오기
        //SkillEffectData.s_skillEffectInfo skillEffectInfo = GameManager.instance.dataManager.GetSkillEffectInfo(skillEffectID);
        SkillEffectData.s_skillEffectInfo skillEffectInfo = GameManager.instance.fileManager.GetSkillEffectInfo(skillEffectID);

        GameObject newEffect = new GameObject(skillEffectInfo._effectID);

        SkillWork.E_TARGET_RANGE targetRange;
        SkillWork.E_TARGET_TYPE targetType;
        SkillWork.E_SITE_TARGET_TYPE siteTargetType;
        SkillWork.E_WORK_TYPE workType;

        switch (skillEffectInfo._target_range)
        {
            case "SINGLE":
                targetRange = SkillWork.E_TARGET_RANGE.SINGLE;
                break;
            case "MULTI":
                targetRange = SkillWork.E_TARGET_RANGE.MULTI;
                break;
            default:
                targetRange = SkillWork.E_TARGET_RANGE.NONE;
                break;
        }
        switch (skillEffectInfo._target_type)
        {
            case "PLAYER":
                targetType = SkillWork.E_TARGET_TYPE.PLAYER;
                break;
            case "MONSTER":
                targetType = SkillWork.E_TARGET_TYPE.MONSTER;
                break;
            case "FRIENDLY":
                targetType = SkillWork.E_TARGET_TYPE.FRIENDLY;
                break;
            case "MY":
                targetType = SkillWork.E_TARGET_TYPE.MY;
                break;
            case "MYMONSTER":
                targetType = SkillWork.E_TARGET_TYPE.MYMONSTER;
                break;
            default:
                targetType = SkillWork.E_TARGET_TYPE.NONE;
                break;
        }
        switch (skillEffectInfo._site_target)
        {
            case "ATTACK_TARGET":
                siteTargetType = SkillWork.E_SITE_TARGET_TYPE.ATTACK_TARGET;
                break;
            case "MY":
                siteTargetType = SkillWork.E_SITE_TARGET_TYPE.MY;
                break;
            default:
                siteTargetType = SkillWork.E_SITE_TARGET_TYPE.NONE;
                break;
        }

        switch (skillEffectInfo._effect_type)
        {
            case "GETATTACKSCORE":
                newEffect.AddComponent<SkillWork>();
                workType = SkillWork.E_WORK_TYPE.NONE;
                break;
            case "DAMAGE":
                newEffect.AddComponent<SkillWork_Damage>();
                workType = SkillWork.E_WORK_TYPE.DAMAGE;
                newEffect.GetComponent<SkillWork_Damage>().SetSkillWork_Damage(skillEffectInfo._dmg_scale, skillEffectInfo._duration, 0);
                break;
            case "DEBUFF":
                SkillWork_BuffDebuff skillWork_BuffDebuff = newEffect.AddComponent<SkillWork_BuffDebuff>();
                workType = SkillWork.E_WORK_TYPE.BUFF;

                GameObject prefabFram = Resources.Load<GameObject>("Prefabs/BuffDebuff/"+ skillEffectInfo._debuff_1);
                skillWork_BuffDebuff.AddBuff(prefabFram.GetComponent<Buff>());
                if(skillEffectInfo._debuff_2 != "")
                {
                    GameObject prefabFram2 = Resources.Load<GameObject>("Prefabs/BuffDebuff/" + skillEffectInfo._debuff_2);
                    skillWork_BuffDebuff.AddBuff(prefabFram2.GetComponent<Buff>());
                }
                if (skillEffectInfo._debuff_3 != "")
                {
                    GameObject prefabFram3 = Resources.Load<GameObject>("Prefabs/BuffDebuff/" + skillEffectInfo._debuff_2);
                    skillWork_BuffDebuff.AddBuff(prefabFram3.GetComponent<Buff>());
                }
                break;
            case "BUFF":
                SkillWork_BuffDebuff skillWork_BuffDebuff2 = newEffect.AddComponent<SkillWork_BuffDebuff>();
                workType = SkillWork.E_WORK_TYPE.BUFF;

                GameObject prefabFram1 = Resources.Load<GameObject>("Prefabs/BuffDebuff/" + skillEffectInfo._buff_1);
                skillWork_BuffDebuff2.AddBuff(prefabFram1.GetComponent<Buff>());
                if (skillEffectInfo._debuff_2 != "")
                {
                    GameObject prefabFram2 = Resources.Load<GameObject>("Prefabs/BuffDebuff/" + skillEffectInfo._buff_2);
                    skillWork_BuffDebuff2.AddBuff(prefabFram2.GetComponent<Buff>());
                }
                if (skillEffectInfo._debuff_3 != "")
                {
                    GameObject prefabFram3 = Resources.Load<GameObject>("Prefabs/BuffDebuff/" + skillEffectInfo._buff_3);
                    skillWork_BuffDebuff2.AddBuff(prefabFram3.GetComponent<Buff>());
                }
                break;
            case "GETHP":
                newEffect.AddComponent<SkillWork_HP>();
                workType = SkillWork.E_WORK_TYPE.HP;
                break;
            case "STRAIGHT":
                SkillWork_Straight skillWork_Straight = newEffect.AddComponent<SkillWork_Straight>();
                workType = SkillWork.E_WORK_TYPE.STRAIGHT;
                SkillEffect_Straight prefab_straight = Resources.Load<SkillEffect_Straight>("Prefabs/Straight/" + skillEffectInfo._straight);
                skillWork_Straight.SetStraightEffect(prefab_straight);
                break;
            case "AOE":
                SkillWork_AOE skillWork_AOE = newEffect.AddComponent<SkillWork_AOE>();
                workType = SkillWork.E_WORK_TYPE.AOE;
                AreaOfEffect prefab_aoe = Resources.Load<AreaOfEffect>("Prefabs/AOE/" + skillEffectInfo._aoe);
                skillWork_AOE.SetAOE(prefab_aoe);
                break;
            case "GETGOLD":
                newEffect.AddComponent<SkillWork_Gold>();
                workType = SkillWork.E_WORK_TYPE.GOLD;
                break;
            case "COOLTIME":
                newEffect.AddComponent<SkillWork_CoolTime>();
                workType = SkillWork.E_WORK_TYPE.COOLTIME;
                break;
            default:
                newEffect.AddComponent<SkillWork>();
                workType = SkillWork.E_WORK_TYPE.NONE;
                break;
        }
        newEffect.GetComponent<SkillWork>().SetSkillWork(targetRange, targetType, skillEffectInfo._site, siteTargetType, workType, parentUnit);

        return newEffect;
    }
 }
