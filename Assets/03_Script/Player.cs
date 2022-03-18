using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Transform _effectPos_HP;
    [SerializeField]
    Transform _effectPos_Gold;

    [SerializeField]
    int _idxDegugGUI;

/*    static Player _unique;
    public static Player _instance { get { return _unique; } }*/

    int _HP = 30;
    int _gold = 600;

    [SerializeField]
    GUI_TopBar _gui_topBar;

    [SerializeField]
    AudioSource _audio_loseHP;

    [SerializeField]
    AudioSource _audio_gainGold;

    public void Hit(int dmg) 
    { 
        _HP -= dmg;
        GameManager.instance.effectManager.CreateTextEffect(_effectPos_HP.position, "-" + dmg.ToString(), EffectManager.E_TEXT_EFFECT_TYPE.DMG_PLAYER);
        _audio_loseHP.Play();
        if (_HP <=0)
        {
            //게임 오버
            Debug.Log("Game Over");
            //GUIManager._instance.SetState(GUIManager.E_GUI_STATE.GAMEOVER);
            GameManager.instance.guiManager.SetState(GUIManager.E_GUI_STATE.GAMEOVER);
        }
        _gui_topBar.SetHPTextP(_HP);
    }

    public void AddHP(int hp)
    {
        _HP += hp;
        if (_HP > 30)
            _HP = 30;
        GameManager.instance.effectManager.CreateTextEffect(_effectPos_HP.position, "-" + hp.ToString(), EffectManager.E_TEXT_EFFECT_TYPE.TOPBAR_HP);
        _gui_topBar.SetHPTextP(_HP);
    }

    public int GetHP() { return _HP; }

    public void AddGold(int inGold)
    {
        _gold += inGold;
        _gui_topBar.SetGoldText(_gold);
        _audio_gainGold.Play();
        GameManager.instance.effectManager.CreateTextEffect(_effectPos_Gold.position, "+" + inGold.ToString(), EffectManager.E_TEXT_EFFECT_TYPE.TOPBAR_GOLD);
    }
    public bool SpendGold(int demand)
    {
        if (_gold - demand >= 0)
        {
            _gold -= demand;
            _gui_topBar.SetGoldText(_gold);
            List<CountCondition> conditionList = GameManager.instance.missionManager.
                GetCondition(CountCondition.E_COUNT_CONDITION_TYPE.USE_GOLD);
            for(int i = 0; i < conditionList.Count; i++)
            {
                conditionList[i].AddCount(demand);
            }
            return true;
        }
        return false;
    }

    private void Awake()
    {
        //_unique = this;
    }

    private void Start()
    {
        _audio_loseHP.volume = 0.5f;
        _audio_gainGold.volume = 0.7f;

        _audio_loseHP.Stop();
        _audio_gainGold.Stop();
    }
    /*    private void OnGUI()
        {
            int nWidth = 100;
            int nHeight = 20;
            int nY = 2;

            GUI.Box(new Rect(nWidth * _idxDegugGUI, nHeight * nY, nWidth, nHeight), string.Format("HP : {0}", _HP)); nY++;
            GUI.Box(new Rect(nWidth * _idxDegugGUI, nHeight * nY, nWidth, nHeight), string.Format("gold : {0}", _gold)); nY++;
        }*/
}
