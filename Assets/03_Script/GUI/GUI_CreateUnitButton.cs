using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_CreateUnitButton : MonoBehaviour
{
    [SerializeField] Text m_txtFunction;

    void SetButton()
    {
        if (TileControl._instance.isTargetSet)
            m_txtFunction.text = "MERGE\nUNIT";
        else
            m_txtFunction.text = "CREATE\nUNIT";
    }

    public void ButtonSetActive(bool command)
    {
        gameObject.SetActive(command);
    }

    private void Update()
    {
        SetButton();
    }
}
