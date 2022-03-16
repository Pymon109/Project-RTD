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

[CreateAssetMenu(fileName = "RoundData", menuName = "Scriptable Object Asset/RoundData")]
public class RoundData : Data
{
    public struct s_roundInfo
    {
        public string _roundID;
        public int _sid;

        public string _property;

        public int _round;
        public int _waitingTime;
        public int _monsterCount;
        public int _rewardGold;

        public s_roundInfo(string roundID, int sid, string property, int round, int waitingTime, int monsterCount, int rewardGold)
        {
            _roundID = roundID;
            _sid = sid;
            _property = property;
            _round = round;
            _waitingTime = waitingTime;
            _monsterCount = monsterCount;
            _rewardGold = rewardGold;
        }
    }

    override internal void UpdateStats(List<GSTU_Cell> list, string RoundID)
    {
        string roundID = "", property = "";
        int sid = 0, round = 0, waitingTime = 0, monsterCount = 0, rewardGold = 0;
        for (int i = 0; i < list.Count; i++)
        {
            switch (list[i].columnId)
            {
                case "_roundID":
                    {
                        roundID = list[i].value;
                        break;
                    }
                case "_property":
                    {
                        property = list[i].value;
                        break;
                    }

                case "_sid":
                    {
                        sid = int.Parse(list[i].value);
                        break;
                    }
                case "_round":
                    {
                        round = int.Parse(list[i].value);
                        break;
                    }
                case "_waitingTime":
                    {
                        waitingTime = int.Parse(list[i].value);
                        break;
                    }
                case "_monsterCount":
                    {
                        monsterCount = int.Parse(list[i].value);
                        break;
                    }
                case "_rewardGold":
                    {
                        rewardGold = int.Parse(list[i].value);
                        break;
                    }
            }
        }
        _h_data.Add(RoundID, new s_roundInfo(roundID, sid, property, round, waitingTime, monsterCount, rewardGold));
    }
}
