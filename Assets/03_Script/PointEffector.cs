using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointEffector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] bool m_bTestHold;

    void SetToolTip(string tag)
    {
        switch (tag)
        {
            case "PointEffector_Game":
                GUI_SystemToolTipData system = GetComponent<GUI_SystemToolTipData>();
                if (system)
                {
                    //Debug.Log("OnPointerEnter_Game");
                    GUI_ToolTipControl._instance.SetToolTip(GUI_ToolTipControl.E_GUI_TOOLTIP.GAME);
                    GUI_ToolTip_System gUI_ToolTip_System = GUI_ToolTipControl._instance.GetToolTipObject(GUI_ToolTipControl.E_GUI_TOOLTIP.GAME).GetComponent<GUI_ToolTip_System>();
                    gUI_ToolTip_System.SetSystemTooltip(system.GetTitle(), system.GetComment(), system.GetKey());
                }
                break;
            case "PointEffector_Skill":
                Skill skill = GetComponent<GUI_SkillIcon>().GetSkill();
                if (skill)
                {
                    //Debug.Log("OnPointerEnter_Skill");
                    GUI_ToolTipControl._instance.SetToolTip(GUI_ToolTipControl.E_GUI_TOOLTIP.SKILL);
                    GUI_ToolTip_Skill gUI_ToolTip_Skill = GUI_ToolTipControl._instance.GetToolTipObject(GUI_ToolTipControl.E_GUI_TOOLTIP.SKILL).GetComponent<GUI_ToolTip_Skill>();
                    gUI_ToolTip_Skill.SetSkillToolTip(skill);
                }
                break;
            case "PointEffector_Buff":
                Buff buff = GetComponent<GUI_BuffIcon>().GetBuff();
                if (buff)
                {
                    Debug.Log("OnPointerEnter_Buff");
                    GUI_ToolTipControl._instance.SetToolTip(GUI_ToolTipControl.E_GUI_TOOLTIP.BUFF_DEBUFF);
                    GUI_ToolTip_Buff gUI_ToolTip_Buff = GUI_ToolTipControl._instance.GetToolTipObject(GUI_ToolTipControl.E_GUI_TOOLTIP.BUFF_DEBUFF).GetComponent<GUI_ToolTip_Buff>();
                    gUI_ToolTip_Buff.SetBuffToolTip(buff);
                }
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (m_bTestHold)
            return;
        SetToolTip(eventData.pointerEnter.tag);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (m_bTestHold)
            return;
        Debug.Log("OnPointerExit");
        GUI_ToolTipControl._instance.SetToolTip(GUI_ToolTipControl.E_GUI_TOOLTIP.NONE);
        //throw new System.NotImplementedException();
    }

    float m_fTouchTime;
    [SerializeField] float m_fMinTouchTime;
    bool m_bIsTouch = false;

    public void DetectTouch()
    {
#if UNITY_EDITOR
        if (m_bTestHold)
        {
            if(Input.GetMouseButtonDown(0))
            {
                m_bIsTouch = true;
                SetToolTip(gameObject.tag);
            }
            else if(Input.GetMouseButtonUp(0))
            {
                m_bIsTouch = false;
                GUI_ToolTipControl._instance.SetToolTip(GUI_ToolTipControl.E_GUI_TOOLTIP.NONE);
            }
            return;
        }
#endif
#if UNITY_EDITOR
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if(touch.phase == TouchPhase.Began)
                {
                    m_bIsTouch = true;
                    SetToolTip(gameObject.tag);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    m_bIsTouch = false;
                    GUI_ToolTipControl._instance.SetToolTip(GUI_ToolTipControl.E_GUI_TOOLTIP.NONE);
                }
            }
        }
#endif
    }

    private void Update()
    {
        DetectTouch();

        if (m_bIsTouch)
            m_fTouchTime += Time.deltaTime;
        else
            m_fTouchTime = 0;
    }
}
