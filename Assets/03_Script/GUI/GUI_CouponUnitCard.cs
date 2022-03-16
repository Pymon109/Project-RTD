using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_CouponUnitCard : MonoBehaviour
{
    [SerializeField]
    List<GUI_CouponUnitCard_Element> _list_element;

    public void SetElement(UnitManager.E_Rank rank)
    {
        int i_rank = (int)rank * 100;
        for (int i = 0; i < 6; i++)
            _list_element[i].SetCard(i_rank + i);
    }
}
