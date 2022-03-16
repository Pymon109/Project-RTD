using UnityEngine;
using System.Collections;
using GoogleSheetsToUnity;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using GoogleSheetsToUnity.ThirdPary;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Data : ScriptableObject
{
    public string associatedSheet = "";
    public string associatedWorksheet = "";

    public void SetSheetAdress(string _associatedSheet, string _associatedWorksheet)
    {
        associatedSheet = _associatedSheet;
        associatedWorksheet = _associatedWorksheet;
    }

    public List<string> _SID = new List<string>();

    protected Hashtable _h_data = new Hashtable();
    public Hashtable GetHashData() { return _h_data; }

    public void StartGetData()
    {
        UpdateStats(UpdateMethodOne);
    }

    bool _isLoadComplete = false;
    public bool IsLoadComplete() { return _isLoadComplete; }

    public void UpdateStats(UnityAction<GstuSpreadSheet> callback, bool mergedCells = false)
    {
        SpreadsheetManager.Read(new GSTU_Search(associatedSheet, associatedWorksheet), callback, mergedCells);
    }

    public void UpdateMethodOne(GstuSpreadSheet ss)
    {
        //data.UpdateStats(ss.rows["Jim"]);
        foreach (string dataID in _SID)
            UpdateStats(ss.rows[dataID], dataID);
        _isLoadComplete = true;
    }

    virtual internal void UpdateStats(List<GSTU_Cell> list, string unitID)
    {

    }
}
