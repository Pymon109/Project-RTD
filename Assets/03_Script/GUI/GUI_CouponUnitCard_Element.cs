using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_CouponUnitCard_Element : MonoBehaviour
{
    [SerializeField]
    Image _img_portrait;

    [SerializeField]
    Text _txt_unitName;

    [SerializeField]
    Text _txt_count;

    [SerializeField]
    Image _img_attribute;

    [SerializeField]
    Image _img_team;

    int _unitSID = -1;
    string[] _sRank = { "Normal", "Magic", "Rare", "Unique", "Epic" };

    public void SetCard(int SID)
    {
        _unitSID = SID;
        int rank = SID / 100;
        //초상화 불러오기
        //Debug.Log("<GUI_CouponUnitCard_Element> SetCard() need to setting portrait image.");
        Sprite portraitIMG = Resources.Load<Sprite>("Sprite/UI/Portrait/_img_portrait_" + SID.ToString());
        if(portraitIMG)
            _img_portrait.sprite = portraitIMG;

        UnitData.s_unitInfo unitInfo = DataManager._instance.GetUnitInfo(SID);
        _txt_unitName.text = unitInfo._name;

        int count = 0;
        count = UnitManager._instance.CountOfThisUnit(SID);
        _txt_count.text = count.ToString();
        if (count < 1)
            _txt_count.gameObject.SetActive(false);
        else
            _txt_count.gameObject.SetActive(true);

        //속성 이미지 불러오기
        Debug.Log("<GUI_CouponUnitCard_Element> SetCard() need to setting attribute image.");


        Sprite teamIMG = Resources.Load<Sprite>("Sprite/UI/TeamIcon/img_" + unitInfo._team + "_icon");
        if (teamIMG)
            _img_team.sprite = teamIMG;
    }

    

    public void ButtonOnCreateUnit()
    {
        if(TileControl._instance.couponUnitCreate(_unitSID))
        {
            CouponManager._instance.ReduceCouponCount(_unitSID / 100);
            TileControl._instance.SwitchCouponMode(false);
        }
    }
}
