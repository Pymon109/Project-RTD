using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBuilder : Builder
{
    public GameObject BuildUnit(int sid, Vector3 position, Quaternion rotation)
    {
        //데이터 가져오기
        UnitData.s_unitInfo unitInfo = DataManager._instance.GetUnitInfo(sid);
        //프리팹 가져오기, 복사
        GameObject prefabFram = Resources.Load<GameObject>("Prefabs/UnitResource/UnitFrame");
        GameObject newFram = Instantiate(prefabFram, position, prefabFram.transform.rotation);
        //모델링 가져오기
        string unitID = "u";
        if (sid < 10)
            unitID += "00";
        else if (sid < 100)
            unitID += "0";
        unitID += sid.ToString();

        GameObject prefabModeling = Resources.Load<GameObject>("Prefabs/UnitResource/Modeling/Modeling_unit_" + unitID);
        GameObject newModeling = Instantiate(prefabModeling, newFram.transform.position, prefabModeling.transform.rotation);
        newModeling.transform.localScale = prefabModeling.transform.localScale;
        newModeling.transform.SetParent(newFram.transform);

        //프로퍼티 컴포넌트 추가, 데이터 셋
        Property property = newFram.AddComponent<Property>();
        Property.E_PROPERTY property_type;
        switch(unitInfo._property)
        {
            case "FIRE":
                property_type = Property.E_PROPERTY.FIRE;
                break;
            case "FOREST":
                property_type = Property.E_PROPERTY.FOREST;
                break;
            case "WARTER":
                property_type = Property.E_PROPERTY.WARTER;
                break;
            default:
                property_type = Property.E_PROPERTY.NONE;
                break;
        }
        property.SetProperty(property_type);

        //유닛 컴포넌트 추가, 데이터 셋
        Unit unit = newFram.AddComponent<Unit>();
        UnitManager.E_Rank rank = UnitManager.E_Rank.NONE; ;
        string s_rank = "";
        int sellPoint = 0;
        switch (unitInfo._rank)
        {
            case "NORMAL":
                rank = UnitManager.E_Rank.NORMAL;
                s_rank = "normal";
                sellPoint = 100;
                break;
            case "MAGIC":
                rank = UnitManager.E_Rank.MAGIC;
                s_rank = "magic";
                sellPoint = 200;
                break;
            case "RARE":
                rank = UnitManager.E_Rank.RARE;
                s_rank = "rare";
                sellPoint = 300;
                break;
            case "UNIQUE":
                rank = UnitManager.E_Rank.UNIQUE;
                s_rank = "unique";
                sellPoint = 400;
                break;
            case "EPIC":
                rank = UnitManager.E_Rank.EPIC;
                s_rank = "epic";
                sellPoint = 500;
                break;
        }
        Unit.E_ATTACK_TYPE attack_type = Unit.E_ATTACK_TYPE.NONE;
        switch(unitInfo._attack_type)
        {
            case "MELEE":
                attack_type = Unit.E_ATTACK_TYPE.MELEE;
                break;
            case "GUN":
                attack_type = Unit.E_ATTACK_TYPE.GUN;
                break;
            case "RANGED":
                attack_type = Unit.E_ATTACK_TYPE.RANGED;
                break;
            case "NONE":
                attack_type = Unit.E_ATTACK_TYPE.NONE;
                break;
        }
        TeamManager.E_TEAM team = TeamManager.E_TEAM.NONE;
        switch (unitInfo._team)
        {
            case "UNION":
                team = TeamManager.E_TEAM.UNION;
                break;
            case "DEMIC":
                team = TeamManager.E_TEAM.DEMIC;
                break;
            case "AXIS":
                team = TeamManager.E_TEAM.AXIS;
                break;
        }
        GameObject effectSite = newFram.transform.Find("effect_site").gameObject;
        unit.SetData(unitInfo._sid, unitInfo._name, rank, team, property, unitInfo._atk, unitInfo._atk_adittional, unitInfo._atk_speed, unitInfo._site,
            sellPoint, effectSite, attack_type);
        Animator newAniCon = newModeling.GetComponent<Animator>();
        unit.SetAniCon(newAniCon);

        //랭크 이펙트 가져오기
        GameObject prefabEffectRank = Resources.Load<GameObject>("Prefabs/UnitResource/effect_bottom/effect_bottom_"+ s_rank);
        GameObject newEffectRank = Instantiate(prefabEffectRank, newFram.transform);
        newEffectRank.transform.SetParent(newFram.transform);

        //유닛 컴포넌트에 이펙트들 연결

        newFram.transform.localScale = Vector3.one * 15;

        //버프 슬롯 연결
        BuffDebuffSlot slot = unit.transform.Find("BuffDebuffSlot").GetComponent<BuffDebuffSlot>();
        unit.SetBuffDebuffSlot(slot);

        //스킬 빌더 호출
        SkillBuilder skillBuilder = (SkillBuilder)DataManager._instance.GetBuilder(DataManager.E_DATATYPE.SKILLDATA);
        GameObject g_skill01, g_skill02 = null;
        Transform skillParent = newFram.transform.Find("Skills");
        g_skill01 = skillBuilder.BuildSkill(unitInfo._skill01, unit);
        g_skill01.transform.SetParent(skillParent);
        if (unitInfo._skill02 != "")
        {
            g_skill02 = skillBuilder.BuildSkill(unitInfo._skill02, unit);
            g_skill02.transform.SetParent(skillParent);
        }

        Skill s_skill01, s_skill02 = null;
        s_skill01 = g_skill01.GetComponent<Skill>();
        if (g_skill02)
            s_skill02 = g_skill02.GetComponent<Skill>();
        unit.SetSkills(s_skill01, s_skill02);

        //사운드 연결
        UnitSounds unitSounds = unit.transform.Find("Sounds").GetComponent<UnitSounds>();
        unit.SetUnitSounds(unitSounds);

        newFram.name = unitInfo._name;
        return newFram;
    }

    public GameObject BuildUnit(UnitData.s_unitInfo unitInfo)
    {
        //프리팹 가져오기, 복사
        GameObject prefabFram = Resources.Load<GameObject>("Prefabs/UnitResource/UnitFrame");
        GameObject newFram = Instantiate(prefabFram);
        //모델링 가져오기
        int sid = unitInfo._sid;
        string unitID = "u";
        if (sid < 10)
            unitID += "00";
        else if (sid < 100)
            unitID += "0";
        unitID += sid.ToString();
        GameObject prefabModeling = Resources.Load<GameObject>("Prefabs/UnitResource/Modeling/Modeling_unit_" + unitID);
        GameObject newModeling = Instantiate(prefabModeling, newFram.transform.position, prefabModeling.transform.rotation);
        newModeling.transform.localScale = prefabModeling.transform.localScale;
        newModeling.transform.SetParent(newFram.transform);

        //프로퍼티 컴포넌트 추가, 데이터 셋
        Property property = newFram.AddComponent<Property>();
        Property.E_PROPERTY property_type;
        switch (unitInfo._property)
        {
            case "FIRE":
                property_type = Property.E_PROPERTY.FIRE;
                break;
            case "FOREST":
                property_type = Property.E_PROPERTY.FOREST;
                break;
            case "WARTER":
                property_type = Property.E_PROPERTY.WARTER;
                break;
            default:
                property_type = Property.E_PROPERTY.NONE;
                break;
        }
        property.SetProperty(property_type);

        //유닛 컴포넌트 추가, 데이터 셋
        Unit unit = newFram.AddComponent<Unit>();
        UnitManager.E_Rank rank = UnitManager.E_Rank.NONE; ;
        string s_rank = "";
        int sellPoint = 0;
        switch (unitInfo._rank)
        {
            case "NORMAL":
                rank = UnitManager.E_Rank.NORMAL;
                s_rank = "normal";
                sellPoint = 100;
                break;
            case "MAGIC":
                rank = UnitManager.E_Rank.MAGIC;
                s_rank = "magic";
                sellPoint = 200;
                break;
            case "RARE":
                rank = UnitManager.E_Rank.RARE;
                s_rank = "rare";
                sellPoint = 300;
                break;
            case "UNIQUE":
                rank = UnitManager.E_Rank.UNIQUE;
                s_rank = "unique";
                sellPoint = 400;
                break;
            case "EPIC":
                rank = UnitManager.E_Rank.EPIC;
                s_rank = "epic";
                sellPoint = 500;
                break;
        }
        Unit.E_ATTACK_TYPE attack_type = Unit.E_ATTACK_TYPE.NONE;
        switch (unitInfo._attack_type)
        {
            case "MELEE":
                attack_type = Unit.E_ATTACK_TYPE.MELEE;
                break;
            case "GUN":
                attack_type = Unit.E_ATTACK_TYPE.GUN;
                break;
            case "RANGED":
                attack_type = Unit.E_ATTACK_TYPE.RANGED;
                break;
            case "NONE":
                attack_type = Unit.E_ATTACK_TYPE.NONE;
                break;
        }
        TeamManager.E_TEAM team = TeamManager.E_TEAM.NONE;
        switch (unitInfo._team)
        {
            case "UNION":
                team = TeamManager.E_TEAM.UNION;
                break;
            case "DEMIC":
                team = TeamManager.E_TEAM.DEMIC;
                break;
            case "AXIS":
                team = TeamManager.E_TEAM.AXIS;
                break;
        }
        GameObject effectSite = newFram.transform.Find("effect_site").gameObject;
        unit.SetData(unitInfo._sid, unitInfo._name, rank, team, property, unitInfo._atk, unitInfo._atk_adittional, unitInfo._atk_speed, unitInfo._site,
            sellPoint, effectSite, attack_type);
        Animator newAniCon = newModeling.GetComponent<Animator>();
        unit.SetAniCon(newAniCon);

        //랭크 이펙트 가져오기
        GameObject prefabEffectRank = Resources.Load<GameObject>("Prefabs/UnitResource/effect_bottom/effect_bottom_" + s_rank);
        GameObject newEffectRank = Instantiate(prefabEffectRank, newFram.transform);
        newEffectRank.transform.SetParent(newFram.transform);

        //유닛 컴포넌트에 이펙트들 연결

        newFram.transform.localScale = Vector3.one * 15;

        //버프 슬롯 연결
        BuffDebuffSlot slot = unit.transform.Find("BuffDebuffSlot").GetComponent<BuffDebuffSlot>();
        unit.SetBuffDebuffSlot(slot);

        //스킬 빌더 호출
        SkillBuilder skillBuilder = (SkillBuilder)DataManager._instance.GetBuilder(DataManager.E_DATATYPE.SKILLDATA);
        GameObject g_skill01, g_skill02 = null;
        Transform skillParent = newFram.transform.Find("Skills");
        g_skill01 = skillBuilder.BuildSkill(unitInfo._skill01, unit);
        g_skill01.transform.SetParent(skillParent);
        if (unitInfo._skill02 != "")
        {
            g_skill02 = skillBuilder.BuildSkill(unitInfo._skill02, unit);
            g_skill02.transform.SetParent(skillParent);
        }

        Skill s_skill01, s_skill02 = null;
        s_skill01 = g_skill01.GetComponent<Skill>();
        if (g_skill02)
            s_skill02 = g_skill02.GetComponent<Skill>();
        unit.SetSkills(s_skill01, s_skill02);

        //사운드 연결
        UnitSounds unitSounds = unit.transform.Find("Sounds").GetComponent<UnitSounds>();
        unit.SetUnitSounds(unitSounds);

        newFram.name = unitInfo._name;
        return newFram;
    }
}
