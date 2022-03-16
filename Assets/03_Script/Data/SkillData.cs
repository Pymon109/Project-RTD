using UnityEngine;
using System.Collections;
using GoogleSheetsToUnity;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using GoogleSheetsToUnity.ThirdPary;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Object Asset/SkillData")]
public class SkillData : Data
{
    public struct s_skillInfo
    {
        public int _sid;
        public string _name;
        public string _trigger_condition;
        public string _effect_1;
        public string _effect_2;
        public string _effect_3;
        public string _comment;

        public float _cooltime;
        public float _cast_chance;
        public float _casting_time;

        public s_skillInfo(int sid, string name, string trigger_condition, float cast_chance, float cooltime, float castingTime,
            string effect_1, string effect_2, string effect_3, string comment)
        {
            _sid = sid;
            _name = name;
            _trigger_condition = trigger_condition;
            _cast_chance = cast_chance;
            _cooltime = cooltime;
            _casting_time = castingTime;
            _effect_1 = effect_1;
            _effect_2 = effect_2;
            _effect_3 = effect_3;
            _comment = comment;
        }
    }

    override internal void UpdateStats(List<GSTU_Cell> list, string SkillID)
    {
        string name = "", trigger_condition = "", effect_1 = "", effect_2 = "", effect_3 = "", comment = "";
        int sid = 0;
        float cast_chance = 0, cooltime = 0, castingTime = 0;
        for (int i = 0; i < list.Count; i++)
        {
            switch (list[i].columnId)
            {
                case "_name":
                    {
                        name = list[i].value;
                        break;
                    }
                case "_trigger_condition":
                    {
                        trigger_condition = list[i].value;
                        break;
                    }
                case "_effect_1":
                    {
                        effect_1 = list[i].value;
                        break;
                    }
                case "_effect_2":
                    {
                        effect_2 = list[i].value;
                        break;
                    }
                case "_effect_3":
                    {
                        effect_3 = list[i].value;
                        break;
                    }
                case "_comment":
                    {
                        comment = list[i].value;
                        break;
                    }


                case "_sid":
                    {
                        sid = int.Parse(list[i].value);
                        break;
                    }
                case "_cooltime":
                    {
                        cooltime = float.Parse(list[i].value);
                        break;
                    }

                case "_cast_chance":
                    {
                        cast_chance = float.Parse(list[i].value);
                        break;
                    }
                case "_casting_time":
                    {
                        castingTime = float.Parse(list[i].value);
                        break;
                    }
            }
        }
        _h_data.Add(SkillID, new s_skillInfo(sid, name, trigger_condition, cast_chance, cooltime, castingTime, effect_1, effect_2, effect_3, comment));
    }
}

/*[CustomEditor(typeof(SkillData))]
public class SkillDataEditor : Editor
{
    SkillData data;

    void OnEnable()
    {
        data = (SkillData)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("Read Data Examples");

        if (GUILayout.Button("Pull Data Method One"))
        {
            UpdateStats(UpdateMethodOne);
        }
    }

    public void UpdateStats(UnityAction<GstuSpreadSheet> callback, bool mergedCells = false)
    {
        SpreadsheetManager.Read(new GSTU_Search(data.associatedSheet, data.associatedWorksheet), callback, mergedCells);
    }

    public void UpdateMethodOne(GstuSpreadSheet ss)
    {
        //data.UpdateStats(ss.rows["Jim"]);
        foreach (string dataSkillID in data._SID)
            data.UpdateStats(ss.rows[dataSkillID], dataSkillID);
        EditorUtility.SetDirty(target);
    }
}*/
