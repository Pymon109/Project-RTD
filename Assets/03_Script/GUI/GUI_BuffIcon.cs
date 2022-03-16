using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_BuffIcon : MonoBehaviour
{
    [SerializeField]
    Buff _buff;
    public void SetBuffl(Buff buff) { _buff = buff; }
    public Buff GetBuff() { return _buff; }
    public void InitBuff() { _buff = null; }
}
