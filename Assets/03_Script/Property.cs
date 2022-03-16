using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Property : MonoBehaviour
{
    public enum E_PROPERTY
    {
        FIRE = 0,
        FOREST,
        WARTER,

        NONE = -1
    }
    [SerializeField]
    E_PROPERTY _property;
    public E_PROPERTY GetProperty() { return _property; }
    public void SetProperty(E_PROPERTY property)
    {
        _property = property;
    }

    public bool IsSameProperty(Property property)
    {
        return _property == property._property;
    }

}
