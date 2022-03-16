using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseControl : MonoBehaviour
{
    [SerializeField]
    GUI_BottomBar _gui_bottomBar;

    [SerializeField]
    GraphicRaycaster m_gr;

    public void DetecteMouseClick()
    {
#if PLATFORM_ANDROID
        if(Input.touchCount > 0)
        {
            for(int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    var ped = new PointerEventData(null);
                    ped.position = touch.position;
                    List<RaycastResult> results = new List<RaycastResult>();
                    m_gr.Raycast(ped, results);

                    if (results.Count <= 0)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                        RaycastHit hit;
                        Vector3 position = new Vector3();

                        if (Physics.Raycast(ray, out hit, 1000.0f, 1 << LayerMask.NameToLayer("Monster")))
                        {
                            //Debug.Log("detect monster : " + hit.collider.name);
                            _gui_bottomBar.SetTargetMonster(hit);

                            TileControl._instance.ReleaseTargetTile();
                        }
                        else if (Physics.Raycast(ray, out hit, 1000.0f, 1 << LayerMask.NameToLayer("GoldMonster")))
                        {
                            //Debug.Log("detect monster : " + hit.collider.name);
                            _gui_bottomBar.SetTargetMonster(hit);

                            TileControl._instance.ReleaseTargetTile();
                        }
                        else if (Physics.Raycast(ray, out hit, 1000.0f, 1 << LayerMask.NameToLayer("Tile")))
                        {
                            //Debug.Log("detect tile : " + hit.collider.name);
                            TileControl._instance.SetTargetTile(hit);
                            _gui_bottomBar.SetTargetUnit(hit);
                        }
                        else
                        {
                            TileControl._instance.ReleaseTargetTile();
                            _gui_bottomBar.SetState(GUI_BottomBar.E_BOTTOMBAR_STATE.NONE);
                            GUI_ToolTipControl._instance.SetToolTip(GUI_ToolTipControl.E_GUI_TOOLTIP.NONE);
                        }
                    }
                    else
                    {

                    }
                }
            }
        }
#endif

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) //마우스 왼쪽 버튼
        {
            var ped = new PointerEventData(null);
            ped.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            m_gr.Raycast(ped, results);

            if(results.Count <= 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Vector3 position = new Vector3();

                if (Physics.Raycast(ray, out hit, 1000.0f, 1 << LayerMask.NameToLayer("Monster")))
                {
                    //Debug.Log("detect monster : " + hit.collider.name);
                    _gui_bottomBar.SetTargetMonster(hit);

                    TileControl._instance.ReleaseTargetTile();
                }
                else if (Physics.Raycast(ray, out hit, 1000.0f, 1 << LayerMask.NameToLayer("GoldMonster")))
                {
                    //Debug.Log("detect monster : " + hit.collider.name);
                    _gui_bottomBar.SetTargetMonster(hit);

                    TileControl._instance.ReleaseTargetTile();
                }
                else if (Physics.Raycast(ray, out hit, 1000.0f, 1 << LayerMask.NameToLayer("Tile")))
                {
                    //Debug.Log("detect tile : " + hit.collider.name);
                    TileControl._instance.SetTargetTile(hit);
                    _gui_bottomBar.SetTargetUnit(hit);
                }
                else
                {
                    TileControl._instance.ReleaseTargetTile();
                    _gui_bottomBar.SetState(GUI_BottomBar.E_BOTTOMBAR_STATE.NONE);
                    GUI_ToolTipControl._instance.SetToolTip(GUI_ToolTipControl.E_GUI_TOOLTIP.NONE);
                }
            }
            else
            {

            }
        }
#endif
    }

    public bool DetectSelectTile()
    {
#if PLATFORM_ANDROID
/*        if (Input.touchCount <= 0)
            return false;

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            if (touch.phase != TouchPhase.Began)
                continue;
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000.0f, 1 << LayerMask.NameToLayer("Tile")))
                    return true;
            }
        }*/
        return false;
#endif
#if UNITY_EDITOR
        if (!Input.GetMouseButtonDown(0)) //마우스 왼쪽 버튼
            return false;

        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000.0f, 1 << LayerMask.NameToLayer("Tile")))
                return true;
        }
        return false;
#endif
    }

    private void Update()
    {
        DetecteMouseClick();
    }
}
