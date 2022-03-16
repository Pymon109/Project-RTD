using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveCondition : MonoBehaviour
{
    public virtual bool ActiveTriger() { return false; }
}
