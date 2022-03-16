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

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Object Asset/UnitData")]
public class UnitData : Data
{
    public struct s_unitInfo
    {
        public int _sid;
        public string _name;
        public string _rank;
        public string _team;
        public string _property;
        public int _atk;
        public int _atk_adittional;
        public float _atk_speed;
        public float _site;
        public string _skill01;
        public string _skill02;
        public string _synergy;
        public string _attack_type;
        public s_unitInfo(int sid, string name, string rank, string team, string property, int atk, int atk_adittional,
            float atk_speed, float site, string skill01, string skill02, string synergy, string attack_type)
        {
            _sid = sid;
            _name = name;
            _rank = rank;
            _team = team;
            _property = property;
            _atk = atk;
            _atk_adittional = atk_adittional;
            _atk_speed = atk_speed;
            _site = site;
            _skill01 = skill01;
            _skill02 = skill02;
            _synergy = synergy;
            _attack_type = attack_type;
        }
    }

    override internal void UpdateStats(List<GSTU_Cell> list, string unitID)
    {
        string name = "", rank = "", team = "", property = "", skill01 = "", skill02 = "", synergy = "", attack_type = "";
        int sid = 0, atk = 0, atk_additional = 0;
        float atk_speed = 0, site = 0;
        for (int i = 0; i < list.Count; i++)
        {
            switch (list[i].columnId)
            {
                case "_name":
                    {
                        name = list[i].value;
                        break;
                    }
                case "_rank":
                    {
                        rank = list[i].value;
                        break;
                    }
                case "_team":
                    {
                        team = list[i].value;
                        break;
                    }
                case "_property":
                    {
                        property = list[i].value;
                        break;
                    }
                case "_skill01":
                    {
                        skill01 = list[i].value;
                        break;
                    }
                case "_skill02":
                    {
                        skill02 = list[i].value;
                        break;
                    }
                case "_synergy":
                    {
                        synergy = list[i].value;
                        break;
                    }
                case "_attack_type":
                    {
                        attack_type = list[i].value;
                        break;
                    }

                case "_sid":
                    {
                        sid = int.Parse(list[i].value);
                        break;
                    }
                case "_atk":
                    {
                        atk = int.Parse(list[i].value);
                        break;
                    }
                case "_atk_adittional":
                    {
                        atk_additional = int.Parse(list[i].value);
                        break;
                    }

                case "_atkSpeed":
                    {
                        atk_speed = float.Parse(list[i].value);
                        break;
                    }
                case "_site":
                    {
                        site = float.Parse(list[i].value);
                        break;
                    }
            }
        }
        _h_data.Add(unitID, new s_unitInfo(sid,name,rank,team,property,atk,atk_additional,atk_speed,site,skill01,skill02,synergy,attack_type));
    }
}