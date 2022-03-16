using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    [SerializeField]
    int _nX;

    [SerializeField]
    int _nY;

    void ShowMeTheMoney()
    {
        Player._instance.AddGold(10000);
    }
    void AddCoupon(UnitManager.E_Rank rank)
    {
        CouponManager._instance.GainCoupon(rank);
    }
    private void OnGUI()
    {
        int nWidth = 200;
        int nHeight = 40;

        int nX = _nX;
        int nY = _nY;

        if (GUI.Button(new Rect(nWidth * nX, nHeight * nY++, nWidth, nHeight), "show me the money"))
            ShowMeTheMoney();
        if (GUI.Button(new Rect(nWidth * nX, nHeight * nY++, nWidth, nHeight), "get rare coupon"))
            AddCoupon(UnitManager.E_Rank.RARE);
        if (GUI.Button(new Rect(nWidth * nX, nHeight * nY++, nWidth, nHeight), "get unique coupon"))
            AddCoupon(UnitManager.E_Rank.UNIQUE);
        if (GUI.Button(new Rect(nWidth * nX, nHeight * nY++, nWidth, nHeight), "get epic coupon"))
            AddCoupon(UnitManager.E_Rank.EPIC);
    }
}
