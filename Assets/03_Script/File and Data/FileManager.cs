using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    //////////////////////////////////////////////////////////////////cvs 읽기
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public List<Dictionary<string, object>> CVSRead(string file)
    {
        var list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load(file) as TextAsset;

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }

    Dictionary<string, UnitData.s_unitInfo> m_dicUnitData = new Dictionary<string, UnitData.s_unitInfo>();
    public UnitData.s_unitInfo GetUnitInfo(string unitId) { return m_dicUnitData[unitId]; }
    private void SetUnitDictionary(Dictionary<string, object> dictionary)
    {
        string unitID = "", name = "", rank = "", team = "", property = "", skill01 = "", skill02 = "", synergy = "", attack_type = "";
        int sid = 0, atk = 0, atk_additional = 0;
        float atk_speed = 0, site = 0;

        unitID = dictionary["_unitID"].ToString();
        sid = (int)dictionary["_sid"];
        name = dictionary["_name"].ToString();
        rank = dictionary["_rank"].ToString();
        team = dictionary["_team"].ToString();
        property = dictionary["_property"].ToString();
        atk = (int)dictionary["_atk"];
        atk_additional = (int)dictionary["_atk_adittional"];
        atk_speed = float.Parse(dictionary["_atkSpeed"].ToString());
        site = float.Parse(dictionary["_site"].ToString());
        skill01 = dictionary["_skill01"].ToString();
        skill02 = dictionary["_skill02"].ToString();
        synergy = dictionary["_synergy"].ToString();
        attack_type = dictionary["_attack_type"].ToString();

        m_dicUnitData.Add(
            unitID,
            new UnitData.s_unitInfo
            (
                sid, name, rank, team, property, atk, atk_additional, atk_speed, site, skill01, skill02, synergy, attack_type
            )
        );
    }

    Dictionary<string, SkillData.s_skillInfo> m_dicSkillData = new Dictionary<string, SkillData.s_skillInfo>();
    public SkillData.s_skillInfo GetSkillInfo(string skillID) { return m_dicSkillData[skillID]; }
    private void SetSkillDictionary(Dictionary<string, object> dictionary)
    {
        string skillID = "", name = "", trigger_condition = "", effect_1 = "", effect_2 = "", effect_3 = "", comment = "";
        int sid = 0;
        float cast_chance = 0, cooltime = 0, castingTime = 0;

        skillID = dictionary["_skillID"].ToString();
        sid = (int)dictionary["_sid"];
        name = dictionary["_name"].ToString();
        trigger_condition = dictionary["_trigger_condition"].ToString();
        cast_chance = float.Parse(dictionary["_cast_chance"].ToString());
        cooltime = float.Parse(dictionary["_cooltime"].ToString());
        effect_1 = dictionary["_effect_1"].ToString();
        effect_2 = dictionary["_effect_2"].ToString();
        effect_3 = dictionary["_effect_3"].ToString();
        castingTime = float.Parse(dictionary["_casting_time"].ToString());
        comment = dictionary["_comment"].ToString();

        m_dicSkillData.Add(
            skillID,
            new SkillData.s_skillInfo
            (
                sid, name, trigger_condition, cast_chance, cooltime, castingTime, effect_1, effect_2, effect_3, comment
            )
        );
    }

    Dictionary<string, SkillEffectData.s_skillEffectInfo> m_dicSkillEffectData = new Dictionary<string, SkillEffectData.s_skillEffectInfo>();
    public SkillEffectData.s_skillEffectInfo GetSkillEffectInfo(string skillEffectID) { return m_dicSkillEffectData[skillEffectID]; }
    private void SetSkillEffectDictionary(Dictionary<string, object> dictionary)
    {
        string effectID = "";
        string target_range = "", target_type = "", site_target = "", effect_type = "";
        string debuff_1 = "", debuff_2 = "", debuff_3 = "";
        string buff_1 = "", buff_2 = "", buff_3 = "", aoe = "", straight = "";
        float site = 0, cooldown_reduction = 0;
        int sid = 0, dmg_scale = 0, duration = 0, debuff_count = 0, buff_count = 0, attack_score = 0, hp = 0, gold = 0;

        effectID = dictionary["_effectID"].ToString();
        sid = (int)dictionary["_sid"];
        target_range = dictionary["_target_range"].ToString();
        target_type = dictionary["_target_type"].ToString();
        site = float.Parse(dictionary["_site"].ToString());
        site_target = dictionary["_site_target"].ToString();
        effect_type = dictionary["_effect_type"].ToString();

        dmg_scale = (int)dictionary["_dmg_scale"];
        duration = (int)dictionary["_duration"];
        
        debuff_count = (int)dictionary["_debuff_count"];
        debuff_1 = dictionary["_debuff_1"].ToString();
        debuff_2 = dictionary["_debuff_2"].ToString();
        debuff_3 = dictionary["_debuff_3"].ToString();

        buff_count = (int)dictionary["_buff_count"];
        buff_1 = dictionary["_buff_1"].ToString();
        buff_2 = dictionary["_buff_2"].ToString();
        buff_3 = dictionary["_buff_3"].ToString();

        attack_score = (int)dictionary["_attack_score"];
        hp = (int)dictionary["_hp"];
        gold = (int)dictionary["_gold"];
        cooldown_reduction = float.Parse(dictionary["_cooldown_reduction"].ToString());
        aoe = dictionary["_AOE"].ToString();
        straight = dictionary["_straight"].ToString();

        m_dicSkillEffectData.Add(
            effectID,
            new SkillEffectData.s_skillEffectInfo
            (
                effectID, sid, target_range, target_type, site_target, effect_type,
                debuff_1, debuff_2, debuff_3, buff_1, buff_2, buff_3,
                aoe, straight, site, cooldown_reduction, dmg_scale, duration,
                debuff_count, buff_count, attack_score, hp, gold
            )
        );
    }

    Dictionary<string, RoundData.s_roundInfo> m_dicRoundData = new Dictionary<string, RoundData.s_roundInfo>();
    public RoundData.s_roundInfo GetRoundInfo(string roundID) { return m_dicRoundData[roundID]; }
    private void SetRoundDictionary(Dictionary<string, object> dictionary)
    {
        string roundID = "", property = "";
        int sid = 0, round = 0, waitingTime = 0, monsterCount = 0, rewardGold = 0;

        roundID = dictionary["_roundID"].ToString();
        sid = (int)dictionary["_sid"];
        round = (int)dictionary["_round"];
        property = dictionary["_property"].ToString();
        waitingTime = (int)dictionary["_waitingTime"];
        monsterCount = (int)dictionary["_monsterCount"];
        rewardGold = (int)dictionary["_rewardGold"];

        m_dicRoundData.Add(
            roundID,
            new RoundData.s_roundInfo
            (
                roundID, sid, property, round, waitingTime, monsterCount, rewardGold
            )
        );
    }


    private void Start()
    {
        List<Dictionary<string, object>> data = CVSRead("Data - Unit");
        for (int i = 0; i < data.Count; i++)
        {
            SetUnitDictionary(data[i]);
        }
        data.Clear();

        data = CVSRead("Data - Skill");
        for (int i = 0; i < data.Count; i++)
        {
            SetSkillDictionary(data[i]);
        }
        data.Clear();

        data = CVSRead("Data - SkillEffect");
        for (int i = 0; i < data.Count; i++)
        {
            SetSkillEffectDictionary(data[i]);
        }
        data.Clear();

        data = CVSRead("Data - Round");
        for (int i = 0; i < data.Count; i++)
        {
            SetRoundDictionary(data[i]);
        }
    }

    bool m_bFileDone = false;
    public bool bFileDone { get { return m_bFileDone; } }
    private void Update()
    {
        if (m_bFileDone)
            return;
        if (!GameManager.instance.objectPoolManager.IsPoolReady())
            return;

        UnitObjectPool unitPool = (UnitObjectPool)GameManager.instance.objectPoolManager.m_pools[(int)ObjectPoolManager.E_POOL_TYPE.UNIT];
        foreach (KeyValuePair<string, UnitData.s_unitInfo> items in m_dicUnitData)
        {
            UnitBuilder unitBuilder = (UnitBuilder)(GameManager.instance.dataManager.GetBuilder(DataManager.E_DATATYPE.UNITDATA));
            unitPool.GetPool((string)items.Key).poolingObject = unitBuilder.BuildUnit((UnitData.s_unitInfo)items.Value);
            unitPool.GetPool((string)items.Key).InitQueu();
        }

        m_bFileDone = true;
        GameManager.instance.guiManager.SetState(GUIManager.E_GUI_STATE.PLAY);
    }
}
