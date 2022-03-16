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

[CreateAssetMenu(fileName = "SkillEffectData", menuName = "Scriptable Object Asset/SkillEffectData")]
public class SkillEffectData : Data
{
    public struct s_skillEffectInfo
    {
        public string _effectID;

        public int _sid;

        public string _target_range;
        public string _target_type;
        public string _site_target;
        public string _effect_type;
        public string _debuff_1;
        public string _debuff_2;
        public string _debuff_3;
        public string _buff_1;
        public string _buff_2;
        public string _buff_3;
        public string _aoe;
        public string _straight;

        public float _site;
        public float _cooldown_reduction;

        public int _dmg_scale;
        public int _duration;
        public int _debuff_count;
        public int _buff_count;
        public int _attack_score;
        public int _hp;
        public int _gold;

        public s_skillEffectInfo(string effectID, int sid,
            string target_range, string target_type, string site_target, string effect_type, string debuff_1,
        string debuff_2, string debuff_3, string buff_1, string buff_2, string buff_3, string aoe, string straight,
        float site, float cooldown_reduction,
        int dmg_scale, int duration, int debuff_count, int buff_count, int attack_score, int hp, int gold)
        {
            _effectID = effectID;
            _sid = sid;
            _target_range = target_range;
            _target_type = target_type;
            _site_target = site_target;
            _effect_type = effect_type;
            _debuff_1 = debuff_1;
            _debuff_2 = debuff_2;
            _debuff_3 = debuff_3;
            _buff_1 = buff_1;
            _buff_2 = buff_2;
            _buff_3 = buff_3;
            _aoe = aoe;
            _straight = straight;
            _site = site;
            _cooldown_reduction = cooldown_reduction;
            _dmg_scale = dmg_scale;
            _duration = duration;
            _debuff_count = debuff_count;
            _buff_count = buff_count;
            _attack_score = attack_score;
            _hp = hp;
            _gold = gold;
        }
    }

    override internal void UpdateStats(List<GSTU_Cell> list, string skillEffectID)
    {
        string effectID = "";
        string target_range = "", target_type = "", site_target = "", effect_type = "";
        string debuff_1 = "", debuff_2 = "", debuff_3 = "";
        string buff_1 = "", buff_2 = "", buff_3 = "", aoe = "", straight = "";
        float site = 0, cooldown_reduction = 0;
        int sid = 0, dmg_scale = 0, duration = 0, debuff_count = 0, buff_count = 0, attack_score = 0, hp = 0, gold = 0;

        for (int i = 0; i < list.Count; i++)
        {
            switch (list[i].columnId)
            {
                case "_effectID":
                    {
                        effectID = list[i].value;
                        break;
                    }
                case "_target_range":
                    {
                        target_range = list[i].value;
                        break;
                    }
                case "_target_type":
                    {
                        target_type = list[i].value;
                        break;
                    }
                case "_site_target":
                    {
                        site_target = list[i].value;
                        break;
                    }
                case "_effect_type":
                    {
                        effect_type = list[i].value;
                        break;
                    }
                case "_debuff_1":
                    {
                        debuff_1 = list[i].value;
                        break;
                    }
                case "_debuff_2":
                    {
                        debuff_2 = list[i].value;
                        break;
                    }
                case "_debuff_3":
                    {
                        debuff_3 = list[i].value;
                        break;
                    }
                case "_buff_1":
                    {
                        buff_1 = list[i].value;
                        break;
                    }
                case "_buff_2":
                    {
                        buff_2 = list[i].value;
                        break;
                    }
                case "_buff_3":
                    {
                        buff_3 = list[i].value;
                        break;
                    }
                case "_AOE":
                    aoe = list[i].value;
                    break;
                case "_straight":
                    straight = list[i].value;
                    break;

                case "_site":
                    {
                        site = float.Parse(list[i].value);
                        break;
                    }
                case "_cooldown_reduction":
                    {
                        cooldown_reduction = float.Parse(list[i].value);
                        break;
                    }


                case "_sid":
                    {
                        sid = int.Parse(list[i].value);
                        break;
                    }
                case "_duration":
                    {
                        duration = int.Parse(list[i].value);
                        break;
                    }
                case "_dmg_scale":
                    {
                        dmg_scale = int.Parse(list[i].value);
                        break;
                    }
                case "_debuff_count":
                    {
                        debuff_count = int.Parse(list[i].value);
                        break;
                    }
                case "_buff_count":
                    {
                        buff_count = int.Parse(list[i].value);
                        break;
                    }
                case "_attack_score":
                    {
                        attack_score = int.Parse(list[i].value);
                        break;
                    }
                case "_hp":
                    {
                        hp = int.Parse(list[i].value);
                        break;
                    }
                case "_gold":
                    {
                        gold = int.Parse(list[i].value);
                        break;
                    }


            }
        }
        _h_data.Add(skillEffectID, new s_skillEffectInfo(effectID, sid, target_range, target_type, site_target, effect_type
            , debuff_1, debuff_2, debuff_3, buff_1, buff_2, buff_3, aoe, straight ,site
            ,cooldown_reduction, dmg_scale, duration, debuff_count, buff_count, attack_score, hp, gold));
    }
}
