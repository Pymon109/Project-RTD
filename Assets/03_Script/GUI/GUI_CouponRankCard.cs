using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_CouponRankCard : MonoBehaviour
{
    [SerializeField]
    Text _txt_count;

    Color _activeColor;
    Color _zeroColor;

    public void SetCount(int count)
    {
        _txt_count.text = "x" + count.ToString();
        if (count > 0)
            _txt_count.color = _activeColor;
        else
            _txt_count.color = _zeroColor;
    }

    private void Start()
    {
        ColorUtility.TryParseHtmlString("#7B7565", out _zeroColor);
        ColorUtility.TryParseHtmlString("#FFFFFF", out _activeColor);
    }
}
