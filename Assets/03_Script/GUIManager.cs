using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    static GUIManager _unique;
    public static GUIManager _instance { get { return _unique; } }

    [SerializeField]
    List<GameObject> _list_gui;

    public enum E_GUI_STATE
    {
        PLAY = 0,
        CLEAR,
        GAMEOVER,
        LOAD
    }

    E_GUI_STATE _currentState = E_GUI_STATE.PLAY;

    void UpdateState()
    {
        switch (_currentState)
        {
            case E_GUI_STATE.PLAY:
                break;
            case E_GUI_STATE.CLEAR:
                break;
            case E_GUI_STATE.GAMEOVER:
                break;
            case E_GUI_STATE.LOAD:
                break;
        }
    }
    public void SetState(E_GUI_STATE command)
    {
        switch(command)
        {
            case E_GUI_STATE.PLAY:
                SoundManager._instance.BGMStart();
                Time.timeScale = 1;
                break;
            case E_GUI_STATE.CLEAR:
                Time.timeScale = 0;
                break;
            case E_GUI_STATE.GAMEOVER:
                Time.timeScale = 0;
                break;
            case E_GUI_STATE.LOAD:
                Time.timeScale = 0;
                break;
        }
        _currentState = command;
        ChangeScreen();
    }
    public void ChangeScreen()
    {
        for(int i = 0; i< _list_gui.Count; i++)
        {
            if ((int)_currentState == i)
                _list_gui[i].SetActive(true);
            else
                _list_gui[i].SetActive(false);
        }
    }

    private void Awake()
    {
        _unique = this;
    }
    private void Start()
    {
        SetState(E_GUI_STATE.LOAD);
    }

    private void Update()
    {
        UpdateState();
    }

}
