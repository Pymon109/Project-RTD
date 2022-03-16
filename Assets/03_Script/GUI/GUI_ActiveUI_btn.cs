﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_ActiveUI_btn : MonoBehaviour
{
    [SerializeField]
    GameObject _target;

    [SerializeField]
    KeyCode _operationKey;

    bool _IsActive;

    public void SwitchActive()
    {
        _IsActive ^= true;
        _target.SetActive(_IsActive);
        CouponManager._instance.UpdateCouponGUI();
        TeamManager._instance.UpdateTeamCardGUI();
    }

    private void Awake()
    {
        _IsActive = _target.activeInHierarchy;
    }

    private void Update()
    {
        if(Input.GetKeyDown(_operationKey))
        {
            SwitchActive();
        }
    }
}
