using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundBuilder : Builder
{
    public GameObject BuildRound(string roundID)
    {
        GameObject newRound = new GameObject(roundID);

        //데이터 가져오기
        RoundData.s_roundInfo roundInfo = DataManager._instance.GetRoundDataInfo(roundID);

        //라운드 스크립트 붙이기
        //monster
        string prefabMonsterPath = "monster_slime_none";
        bool isBossRound = false;
        MonsterObjectPool.E_MONSTER_TYPE property = MonsterObjectPool.E_MONSTER_TYPE.SLIME_FIRE;
        switch (roundInfo._property)
        {
            case "FIRE":
                prefabMonsterPath = "monster_slime_fire";
                property = MonsterObjectPool.E_MONSTER_TYPE.SLIME_FIRE;
                break;
            case "WARTER":
                prefabMonsterPath = "monster_slime_warter";
                property = MonsterObjectPool.E_MONSTER_TYPE.SLIME_WARTER;
                break;
            case "FOREST":
                prefabMonsterPath = "monster_slime_forest";
                property = MonsterObjectPool.E_MONSTER_TYPE.SLIME_FOREST;
                break;
            case "BOSS":
                //기본 보스로 셋팅하고 추후에 바꾸는 로직
                prefabMonsterPath = "BossMonster/monster_boss_robot";
                property = MonsterObjectPool.E_MONSTER_TYPE.BOSS_ROBOT;
                isBossRound = true;
                break;
        }
        GameObject prefabMonsterObj = Resources.Load<GameObject>("Prefabs/Monster/" + prefabMonsterPath);
        //Round round = new Round(roundInfo._round, roundInfo._waitingTime, prefabMonsterObj, roundInfo._monsterCount, roundInfo._rewardGold, isBossRound);
        Round round = newRound.AddComponent<Round>();
        round.SetRound(roundInfo._round, roundInfo._waitingTime, prefabMonsterObj, roundInfo._monsterCount, roundInfo._rewardGold, isBossRound);
        round.property = property;
        newRound.name = roundInfo._round.ToString();
        newRound.transform.SetParent(RoundManager._instance.gameObject.transform);

        return newRound;
    }
}
