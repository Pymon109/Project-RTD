using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_TeamCard : MonoBehaviour
{
    [SerializeField]
    List<Text> _list_txt_unitCount;

    [SerializeField]
    Text _txt_level;

    [SerializeField]
    Text _txt_gold;

    Color _activeColor;
    Color _zeroColor;

    public void InitUnitCount(int[] count)
    {
        for(int rank = 0; rank < 5; rank++)
        {
            _list_txt_unitCount[rank].text = count[rank].ToString();
            if (count[rank] > 0)
                _list_txt_unitCount[rank].color = _activeColor;
            else
                _list_txt_unitCount[rank].color = _zeroColor;
        }
    }

    public void SetUnitCount(UnitManager.E_Rank rank, int count)
    {
        _list_txt_unitCount[(int)rank].text = count.ToString();
        if(count>0)
            _list_txt_unitCount[(int)rank].color = _activeColor;
        else
            _list_txt_unitCount[(int)rank].color = _zeroColor;
    }
    public void SetLevel(int level)
        {
        _txt_level.text = "Lv. " + level.ToString();
    }
    public void SetGold(int gold)
    {
        _txt_gold.text = gold.ToString();
    }

    private void Start()
    {
        ColorUtility.TryParseHtmlString("#7B7565", out _zeroColor);
        ColorUtility.TryParseHtmlString("#FFFFFF", out _activeColor);
    }
}
