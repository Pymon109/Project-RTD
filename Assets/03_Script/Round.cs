using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour
{
    /*    [SerializeField]
        int _idxDegugGUI;*/

    [SerializeField]
    int _round;

    [SerializeField]
    float _waitingTime;
    public float GetWaitingTime() { return _waitingTime; }
    public void ReduceWaitinTime() { _waitingTime--; }

    [SerializeField]
    GameObject _spawnMonster;

    MonsterObjectPool.E_MONSTER_TYPE m_property;
    public MonsterObjectPool.E_MONSTER_TYPE property { set { m_property = value; } }


    [SerializeField]
    int _monsterCount;
    bool _isSpawnning = false;

    [SerializeField]
    int _rewardGold;

    [SerializeField]
    bool _isBossRound;
    public bool IsBossRound() { return _isBossRound; }

    [SerializeField]
    float _site = 150;

    public IEnumerator SpawnMonster()
    {
        _isSpawnning = true;
        for (int i = 0; i < _monsterCount; i++)
        {
            //Spawner._instance.SpawnMonster(_spawnMonster, _round);
            GameManager.instance.spawner.SpawnMonster(m_property, _round);

            yield return new WaitForSeconds(0.5f);
        }
        _isSpawnning = false;
    }

    public bool ProcessFindTarget(string layername)
    {
        int nLayer = 1 << LayerMask.NameToLayer("Monster");
        Collider[] collider = Physics.OverlapSphere(transform.position, _site, nLayer);
        if (collider.Length == 0)
        {
            if(!_isSpawnning)
            {
                GameManager.instance.player.AddGold(_rewardGold);
                return true;
            }
        }
        return false;
    }

    public Round(int round, float waitingTime, GameObject spawnMonster, int monsterCount, int rewardGold, bool isBossRound)
    {
        _round = round;
        _waitingTime = waitingTime;
        _monsterCount = monsterCount;
        _spawnMonster = spawnMonster;
        _monsterCount = monsterCount;
        _rewardGold = rewardGold;
        _isBossRound = isBossRound;
        _site = 150;
    }
    public void SetRound(int round, float waitingTime, GameObject spawnMonster, int monsterCount, int rewardGold, bool isBossRound)
    {
        _round = round;
        _waitingTime = waitingTime;
        _monsterCount = monsterCount;
        _spawnMonster = spawnMonster;
        _monsterCount = monsterCount;
        _rewardGold = rewardGold;
        _isBossRound = isBossRound;
        _site = 150;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(transform.position, _site);
    }
}
