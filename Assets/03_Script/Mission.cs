using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    [SerializeField]
    int _sid;
    public int GetSID() { return _sid; }
    [SerializeField]
    string _name;
    [SerializeField]
    string _description;
    [SerializeField]
    int _maxCount;
    public int GetMaxCount() { return _maxCount; }
    int _currentCount = 0;
    public int GetCurrentCount() { return _currentCount; }
    [SerializeField]
    CountCondition _countCondition;
    public CountCondition GetCountCondition() { return _countCondition; }
    [SerializeField]
    ActiveCondition _activeCondition;
    [SerializeField]
    GUI_MissionCard _gui_missionCard;
    public GUI_MissionCard GetGUI_MissionCard() { return _gui_missionCard; }
    [SerializeField]
    GameObject _mission_content;

    public enum E_MISSION_REWARD
    {
        GOLD = 0,
        MAGIC,
        RARE,
        UNIQUE,
        EPIC
    }
    [SerializeField]
    E_MISSION_REWARD _rewardType;
    [SerializeField]
    int _rewardCount;

    bool _IsActive = false;
    public bool IsActive() { return _IsActive; }
    void SetActiveMission(bool command)
    {
        switch(command)
        {
            case true:
                GameObject prefap = Resources.Load<GameObject>("Prefabs/" + _gui_missionCard.name);
                GameObject newobj = Instantiate(prefap, _mission_content.transform);
                //newobj.transform.SetParent(_mission_content.transform);

                _gui_missionCard = newobj.GetComponent<GUI_MissionCard>();

                _gui_missionCard.SetMissionName(_name);
                _gui_missionCard.SetMissionDescription(_description);
                _gui_missionCard.SetMissionCount(_maxCount, _currentCount);
                _gui_missionCard.SetRewardCount(_rewardCount);
                _gui_missionCard.SetRewadImage(_rewardType);
                break;
            case false:
                Destroy(_gui_missionCard.gameObject);
                break;
        }
        _IsActive = command;
    }

    void Reward()
    {
        switch(_rewardType)
        {
            case E_MISSION_REWARD.GOLD:
                Player._instance.AddGold(_rewardCount);
                //이펙트 필요
                break;
            case E_MISSION_REWARD.MAGIC:
                //이펙트 필요
                break;
            case E_MISSION_REWARD.RARE:
                //이펙트 필요
                CouponManager._instance.GainCoupon(UnitManager.E_Rank.RARE);
                break;
            case E_MISSION_REWARD.UNIQUE:
                //이펙트 필요
                CouponManager._instance.GainCoupon(UnitManager.E_Rank.UNIQUE);
                break;
            case E_MISSION_REWARD.EPIC:
                //이펙트 필요
                CouponManager._instance.GainCoupon(UnitManager.E_Rank.EPIC);
                break;
        }
        MissionManager._instance.PlayMissionCompleteAudio();
    }

    private void Update()
    {
        if(!_IsActive)
        {
            if (_activeCondition)
                _IsActive = _activeCondition.ActiveTriger();
            else
                _IsActive = true;
            if(_IsActive)
            {
                SetActiveMission(true);
            }
        }
        else
        {
            _currentCount = _countCondition.CurrentCount();
            _gui_missionCard.SetMissionCount(_maxCount, _currentCount);
            if(_currentCount >= _maxCount)
            {
                //미션 완료
                Reward();
                SetActiveMission(false);
                gameObject.SetActive(false);
            }
        }
    }

    private void Start()
    {
        /*GameObject prefap = Resources.Load<GameObject>("Prefabs/" + _gui_missionCard.name);
        GameObject newobj = Instantiate(prefap);
        newobj.transform.SetParent(_mission_content.transform);

        _gui_missionCard = newobj.GetComponent<GUI_MissionCard>();

        _gui_missionCard.SetMissionName(_name);
        _gui_missionCard.SetMissionDescription(_description);
        _gui_missionCard.SetMissionCount(_maxCount, _currentCount);
        _gui_missionCard.SetRewardCount(_rewardCount);
        _gui_missionCard.SetRewadImage(_rewardType);*/
    }
}
