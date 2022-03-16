using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    static MissionManager _unique;
    public static MissionManager _instance { get { return _unique; } }

    List<Mission> _list_missions;

    [SerializeField]
    AudioSource _audio_missionComplete;

    public void PlayMissionCompleteAudio() { _audio_missionComplete.Play(); }

    public List<CountCondition> GetCondition(CountCondition.E_COUNT_CONDITION_TYPE type)
    {
        List<CountCondition> listCondition = new List<CountCondition>();
        for(int i = 0; i < _list_missions.Count; i++)
        {
            if (_list_missions[i].IsActive())
            {
                CountCondition condition = _list_missions[i].GetCountCondition();
                if (condition.GetConditionType() == type)
                    listCondition.Add(condition);
            }
        }
        return listCondition;
    }

    private void Awake()
    {
        _unique = this;
        _list_missions = new List<Mission>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Mission mission = transform.GetChild(i).gameObject.GetComponent<Mission>();
            if(mission)
                _list_missions.Add(mission);
        }
    }

    private void Start()
    {
        _audio_missionComplete.volume = 0.5f;
        _audio_missionComplete.Stop();
    }
}
