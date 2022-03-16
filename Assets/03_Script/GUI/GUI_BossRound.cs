using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_BossRound : MonoBehaviour
{
    [SerializeField]
    GUI_StatusBar _statusBar_boss;
    public GUI_StatusBar GetBossStatusBar() { return _statusBar_boss; }

    [SerializeField]
    Text _txt_bossName;
    public void SetBossName(string name) { _txt_bossName.text = name; }

    [SerializeField]
    GameObject _warningSign;

    public IEnumerator ActiveWarningSign()
    {
        _warningSign.SetActive(true);
        yield return new WaitForSeconds(2f);
        _warningSign.SetActive(false);
    }
}
