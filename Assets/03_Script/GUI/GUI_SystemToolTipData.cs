using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_SystemToolTipData : MonoBehaviour
{
    [SerializeField]
    string _title;
    public string GetTitle() { return _title; }
    [SerializeField]
    string _comment;
    public string GetComment() { return _comment; }
    [SerializeField]
    string _key;
    public string GetKey() { return _key; }

}
