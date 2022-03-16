using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_UnitCoupon : MonoBehaviour
{
    [SerializeField]
    Text _count;

    public void SetCouponCount(int count)
    {
        if (count < 0)
            _count.gameObject.SetActive(false);
        else
        {
            _count.gameObject.SetActive(true);
            _count.text = "x"+count.ToString();
        }
    }
}
