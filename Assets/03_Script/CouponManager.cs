using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouponManager : MonoBehaviour
{
    [SerializeField]
    int[] _couponCount = new int[5];
    public void ReduceCouponCount(int rank)
    {
        _couponCount[rank]--;
        UpdateCouponGUI();
    }


    [SerializeField]
    GUI_CouponRankCard[] _gui_unitCoupons = new GUI_CouponRankCard[5];
    GUI_UnitCoupon _gui_unitCoupon;
    [SerializeField]
    GUI_CouponUnitCard _gui_couponUnitCard;


    public void SwitchCouponUnitCard(bool command, Vector3 pos)
    {
        _gui_couponUnitCard.gameObject.SetActive(command);
        if(command)
        {
            _gui_couponUnitCard.transform.position = pos;
        }
    }

    public void ButtonOnEpicCoupon()
    {
        UseCoupon(UnitManager.E_Rank.EPIC);
    }
    public void ButtonOnUniqueCoupon()
    {
        UseCoupon(UnitManager.E_Rank.UNIQUE);
    }
    public void ButtonOnRareCoupon()
    {
        UseCoupon(UnitManager.E_Rank.RARE);
    }
    public void ButtonOnCancel()
    {
        GameManager.instance.tileManager.SwitchCouponMode(false);
    }
    public void UseCoupon(UnitManager.E_Rank rank)
    {
        //Debug.Log("UseCoupon "+ rank);
        if(_couponCount[(int)rank]>0)
        {
            GameManager.instance.tileManager.SwitchCouponMode(true);
            _gui_couponUnitCard.SetElement(rank);
        }
            
    }

    public void GainCoupon(UnitManager.E_Rank rank)
    {
        _couponCount[(int)rank]++;
        UpdateCouponGUI();
    }

    public void UpdateCouponGUI()
    {
        int count = 0;
        for (int i = 0; i < _couponCount.Length; i++)
        {
            count += _couponCount[i];
            if(_gui_unitCoupons[i])
                _gui_unitCoupons[i].SetCount(_couponCount[i]);
        }
        _gui_unitCoupon.SetCouponCount(count);
    }

    private void Awake()
    {
        _gui_unitCoupon = GetComponent<GUI_UnitCoupon>();
    }

}
