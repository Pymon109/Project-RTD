using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_MissionCard : MonoBehaviour
{
    [SerializeField]
    Text _txt_name;
    public void SetMissionName(string name) { _txt_name.text = name; }

    [SerializeField]
    Text _txt_description;
    public void SetMissionDescription(string description) { _txt_description.text = description; }

    [SerializeField]
    Text _txt_missionCount;
    public void SetMissionCount(int maxCount, int curCount) { _txt_missionCount.text = curCount.ToString() + "/" + maxCount.ToString(); }

    [SerializeField]
    Text _txt_rewardCount;
    public void SetRewardCount(int count) { _txt_rewardCount.text = count.ToString(); }

    [SerializeField]
    Image _img_reward;
    public void SetRewadImage(Mission.E_MISSION_REWARD rewardType)
    {
        switch(rewardType)
        {
            case Mission.E_MISSION_REWARD.GOLD:
                _img_reward.sprite = Resources.Load<Sprite>("Sprite/UI/img_icon_gold");
                break;
            case Mission.E_MISSION_REWARD.MAGIC:
                _img_reward.sprite = Resources.Load<Sprite>("Sprite/UI/img_icon_unit");
                break;
            case Mission.E_MISSION_REWARD.RARE:
                _img_reward.sprite = Resources.Load<Sprite>("Sprite/UI/img_icon_unit");
                break;
            case Mission.E_MISSION_REWARD.UNIQUE:
                _img_reward.sprite = Resources.Load<Sprite>("Sprite/UI/img_icon_unit");
                break;
            case Mission.E_MISSION_REWARD.EPIC:
                _img_reward.sprite = Resources.Load<Sprite>("Sprite/UI/img_icon_unit");
                break;
        }
    }

    
}
