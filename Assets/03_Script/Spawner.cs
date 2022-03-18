using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject _path;
    public GameObject GetAllPath() { return _path; }

    [SerializeField] GameObject _testGoldMonster;

    [SerializeField] float _coolTime_goldMonster;
    bool _isCoolTimeDown_goldMonster = true;

    [SerializeField] GUI_BossRound _gui_bossRound;

    [SerializeField] GUI_GoldMonsterButton _gui_goldMonsterButton;

    public GameObject SpawnMonster(GameObject g_monster, int level)
    {
        GameObject newobj = Instantiate(g_monster, transform.position, transform.rotation);
        newobj.GetComponent<MonsterAI>().SetPath(_path);

        Monster monster = newobj.GetComponent<Monster>();
        if (monster && monster.IsBoss())
        {
            monster.SetStatusBar(_gui_bossRound.GetBossStatusBar());
            _gui_bossRound.SetBossName(monster.GetName());
        }
        monster.SetLevel(level);
        return newobj;
    }

    public GameObject SpawnMonster(MonsterObjectPool.E_MONSTER_TYPE property, int level)
    {
        //MonsterObjectPool monsterPool = (MonsterObjectPool)ObjectPoolManager.instance.m_pools[(int)ObjectPoolManager.E_POOL_TYPE.MONSTER];
        MonsterObjectPool monsterPool = (MonsterObjectPool)GameManager.instance.objectPoolManager.
            m_pools[(int)ObjectPoolManager.E_POOL_TYPE.MONSTER];
        //GameObject poolMonster = monsterPool.GetPool(property).GetObject(ObjectPoolManager.instance.trsDynamicObjects);
        GameObject poolMonster = monsterPool.GetPool(property).GetObject(
            GameManager.instance.objectPoolManager.trsDynamicObjects);

        poolMonster.transform.position = transform.position;
        poolMonster.GetComponent<MonsterAI>().SetPath(_path);

        Monster monster = poolMonster.GetComponent<Monster>();
        monster.InitMonster();
        if (monster && monster.IsBoss())
        {
            monster.SetStatusBar(_gui_bossRound.GetBossStatusBar());
            _gui_bossRound.SetBossName(monster.GetName());
        }
        monster.SetLevel(level);
        return poolMonster;
    }

    IEnumerator GoldMonsterProcess()
    {
        if(_isCoolTimeDown_goldMonster)
        {
            _gui_goldMonsterButton.ButtonSetActive(false);
            _isCoolTimeDown_goldMonster = false;
            SpawnMonster(MonsterObjectPool.E_MONSTER_TYPE.GOLD_MONSTER,
                GameManager.instance.goldMonsterManaer.GetNextLevel());
            List<CountCondition> conditionList = GameManager.instance.missionManager.
                GetCondition(CountCondition.E_COUNT_CONDITION_TYPE.SPAWN_GOLDMONSTER);
            for (int i = 0; i < conditionList.Count; i++)
            {
                conditionList[i].AddCount(1);
            }
            int currentTime = (int)_coolTime_goldMonster;
            while (currentTime > 0)
            {
                _gui_goldMonsterButton.SetCountDown(currentTime);
                yield return new WaitForSeconds(1.0f);
                currentTime--;
            }
            _gui_goldMonsterButton.ButtonSetActive(true);
            _isCoolTimeDown_goldMonster = true;
        }
    }

    [SerializeField]
    List<GameObject> _effects;

    public enum E_SPAWNER_STATE
    {
        WAIT = 0,
        SPAWNING,
        SPAWN_END
    }
    E_SPAWNER_STATE _currentSpawnerState = E_SPAWNER_STATE.WAIT;

    void UpdateState()
    {
        switch(_currentSpawnerState)
        {
            case E_SPAWNER_STATE.WAIT:
                break;
            case E_SPAWNER_STATE.SPAWNING:
                break;
            case E_SPAWNER_STATE.SPAWN_END:
                break;
        }
    }

    public void SetState(E_SPAWNER_STATE command)
    {
        switch (command)
        {
            case E_SPAWNER_STATE.WAIT:
                break;
            case E_SPAWNER_STATE.SPAWNING:
                break;
            case E_SPAWNER_STATE.SPAWN_END:
                StartCoroutine(WaitForPawnEndEffect());
                break;
        }
        SwitchEffect((int)command);
    }

    void SwitchEffect(int index)
    {
        for(int i = 0; i < _effects.Count; i++)
        {
            if (i == index)
                _effects[i].SetActive(true);
            else
                _effects[i].SetActive(false);
        }
    }

    IEnumerator WaitForPawnEndEffect()
    {
        yield return new WaitForSeconds(1.7f);
        SetState(E_SPAWNER_STATE.WAIT);
    }


    public void ButtonOnGoldMonster()
    {
        StartCoroutine(GoldMonsterProcess());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ButtonOnGoldMonster();
        }
        UpdateState();
    }
}
