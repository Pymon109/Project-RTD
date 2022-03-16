using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    [SerializeField]
    int _sid;
    public int GetSID() { return _sid; }

    [SerializeField]
    string _buffID;
    public string GetBuffID() { return _buffID; }

    [SerializeField]
    protected string _name;
    public string GetName() { return _name; }

    [SerializeField]
    string _comment = "test comment. test comment. test comment. test comment. ";
    public string GetComment() { return _comment; }
    
    [SerializeField]
    bool _isDebuff;
    public bool IsDebuff() { return _isDebuff; }

    [SerializeField]
    protected float _duration;
    [SerializeField]
    protected float _cycle;
    [SerializeField]
    bool _isStackable;
    public bool IsStackable() { return _isStackable; }

    protected GameObject _target;
    public void SetTarget(GameObject target) { _target = target; }

    protected TeamManager.E_TEAM _team;
    public void SetTeam(TeamManager.E_TEAM team) { _team = team; }

    protected Property _property;
    public void SetProperty(Property property) { _property = property; }

    virtual public void BuffOn()
    {

    }
    virtual public void BuffOff()
    {


    }
}
