using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_StatusBar : MonoBehaviour
{
    [SerializeField] RectTransform _rectTransform;
    [SerializeField] Transform _pos_target;
    [SerializeField] Vector2 _vMaxBarSize;

    public void SetTarget(Transform target)
    {
        _pos_target = target;
    }

    public void SetState(float cur, float max)
    {
        float rat = cur / max;
        Vector2 vSize = _rectTransform.sizeDelta;
        vSize.x = _vMaxBarSize.x * rat;
        _rectTransform.sizeDelta = vSize;

    }

    // Start is called before the first frame update
    void Start()
    {
        //_rectTransform.sizeDelta = new Vector2(120, 20);
        _vMaxBarSize = _rectTransform.sizeDelta;
        SetState(1, 1);
    }

    private void Update()
    {
        if(_pos_target)
            transform.position = _pos_target.transform.position;
    }
}
