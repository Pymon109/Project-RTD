using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static DataManager _unique;
    public static DataManager _instance { get { return _unique; } }

    public enum E_DATATYPE
    {
        UNITDATA = 0,
        SKILLDATA,
        SKILLEFFECTDATA,
        BUFF,
        ROUND,
        MISSION,

        COMPLETE,
        DONE,
        WAIT = -1
    }
    bool _isDataLoadEnd = false;

    [SerializeField]
    string associatedSheet = "";
    [SerializeField]
    List<string> associatedWorksheet = new List<string>();

    [SerializeField]
    List<Data> _datas;

    [SerializeField]
    List<Builder> _builders = new List<Builder>();
    public Builder GetBuilder(E_DATATYPE type) { return _builders[(int)type]; }

    List<Hashtable> _tables = new List<Hashtable>();

    [SerializeField]
    int TestUnitBuild;
    public UnitData.s_unitInfo GetUnitInfo(int sid)
    {
        string unitID = "u";
        if (sid < 10)
            unitID += "00";
        else if (sid < 100)
            unitID += "0";
        unitID += sid.ToString();

        return (UnitData.s_unitInfo)_tables[(int)E_DATATYPE.UNITDATA][unitID];
    }
    public SkillData.s_skillInfo GetSkillInfo(string skillID)
    {
        return (SkillData.s_skillInfo)_tables[(int)E_DATATYPE.SKILLDATA][skillID];
    }

    public SkillEffectData.s_skillEffectInfo GetSkillEffectInfo(string skillEffectID)
    {
        return (SkillEffectData.s_skillEffectInfo)_tables[(int)E_DATATYPE.SKILLEFFECTDATA][skillEffectID];
    }

    public RoundData.s_roundInfo GetRoundDataInfo(string roundID)
    {
        return (RoundData.s_roundInfo)_tables[(int)E_DATATYPE.ROUND][roundID];
    }

    List<bool> _isLoadStart = new List<bool>();
    List<bool> _isLoadData = new List<bool>();
    public bool IsLoadData(E_DATATYPE type) { return _isLoadData[(int)type]; }

    void DataLoading(E_DATATYPE type)
    {
        int idx = (int)type;
        if (!_isLoadData[idx])
        {
            if (_tables[idx].Count == 0)
                _tables[idx] = _datas[idx].GetHashData();
            else
            {
                _isLoadData[idx] = true;
                DebugLogData(type);

                //테스트 코드 빌드
            }
        }
    }

    void DebugLogData(E_DATATYPE type)
    {
        int idx = (int)type;
        switch(type)
        {
            case E_DATATYPE.UNITDATA:
                foreach (string nowKey in _tables[idx].Keys)
                {
                    UnitData.s_unitInfo unitInfo = (UnitData.s_unitInfo)_tables[idx][nowKey];
                    Debug.Log($"{nowKey} / {unitInfo._sid} / {unitInfo._name} / {unitInfo._rank} / {unitInfo._team} / " +
                        $"{unitInfo._property} / {unitInfo._atk}(+{unitInfo._atk_adittional}) / {unitInfo._atk_speed} / " +
                        $"{unitInfo._site} / {unitInfo._skill01} / {unitInfo._skill02} / {unitInfo._synergy}");
                }
                break;
            case E_DATATYPE.SKILLDATA:
                foreach (string nowKey in _tables[idx].Keys)
                {
                    SkillData.s_skillInfo skillInfo = (SkillData.s_skillInfo)_tables[idx][nowKey];
                    Debug.Log($"{nowKey} / {skillInfo._name} / {skillInfo._trigger_condition} / {skillInfo._cast_chance} / " +
                        $"{skillInfo._cooltime} / {skillInfo._effect_1} / {skillInfo._effect_2} / {skillInfo._effect_3}");
                }
                break;
            case E_DATATYPE.SKILLEFFECTDATA:
                foreach (string nowKey in _tables[idx].Keys)
                {
                    SkillEffectData.s_skillEffectInfo skillEffectInfo = (SkillEffectData.s_skillEffectInfo)_tables[idx][nowKey];
                    Debug.Log($"{nowKey}");
                }
                break;
            case E_DATATYPE.ROUND:
                foreach (string nowKey in _tables[idx].Keys)
                {
                    RoundData.s_roundInfo roundInfo = (RoundData.s_roundInfo)_tables[idx][nowKey];
                    Debug.Log($"{nowKey}");
                }
                break;
        }
    }

    private void Awake()
    {
        _unique = this;
    }

    private void Start()
    {
        for (int i = 0; i < _datas.Count; i++)
        {
            _isLoadStart.Add(false);
            _isLoadData.Add(false);
            _tables.Add(new Hashtable());
            if (_datas[i])
                _datas[i].SetSheetAdress(associatedSheet, associatedWorksheet[i]);
        }

        //_datas[0].StartGetData();
    }

    bool m_bIsUnitPoolDone = false;
    private void Update()
    {
        E_DATATYPE command = E_DATATYPE.WAIT;

        if (!_isLoadData[(int)E_DATATYPE.UNITDATA])
            command = E_DATATYPE.UNITDATA;
        else if (!_isLoadData[(int)E_DATATYPE.SKILLDATA])
            command = E_DATATYPE.SKILLDATA;
        else if (!_isLoadData[(int)E_DATATYPE.SKILLEFFECTDATA])
            command = E_DATATYPE.SKILLEFFECTDATA;
        else if (!_isLoadData[(int)E_DATATYPE.ROUND])
            command = E_DATATYPE.ROUND;
        else
        {
            command = E_DATATYPE.COMPLETE;
            if(!_isDataLoadEnd)
            {
                if(!m_bIsUnitPoolDone)
                {
                    UnitObjectPool unitPool = (UnitObjectPool)ObjectPoolManager.instance.m_pools[(int)ObjectPoolManager.E_POOL_TYPE.UNIT];
                    Hashtable unitDataTable = _tables[(int)E_DATATYPE.UNITDATA];
                    foreach (DictionaryEntry entry in unitDataTable)
                    {
                        UnitBuilder unitBuilder = (UnitBuilder)(GetBuilder(E_DATATYPE.UNITDATA));
                        unitPool.GetPool((string)entry.Key).poolingObject = unitBuilder.BuildUnit((UnitData.s_unitInfo)entry.Value);
                        unitPool.GetPool((string)entry.Key).InitQueu();
                    }
                    m_bIsUnitPoolDone = true;
                }
                GUIManager._instance.SetState(GUIManager.E_GUI_STATE.PLAY);
                _isDataLoadEnd = true;
            }
            return;
        }

        if (!_isLoadStart[(int)command])
        {
            _isLoadStart[(int)command] = true;
            _datas[(int)command].StartGetData();
        }
        else
            DataLoading(command);

    }
}
