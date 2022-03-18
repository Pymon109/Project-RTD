using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    TextMeshPro _tmPro;

    float _destroyTime = 0.7f;
    public Color _alpha;
    float _alphaSpeed = 1f;

    bool _trigger = false;

    public void SetText(string text, Color color)
    {
        _tmPro.text = text;
        _alpha = color;

        _tmPro.color = _alpha;
        Invoke("DestroyText", _destroyTime);
        _trigger = true;
    }

    void ColorChange()
    {
        _alpha.a = Mathf.Lerp(_alpha.a, 0, _alphaSpeed * Time.deltaTime);
        _tmPro.color = _alpha;
        _tmPro.outlineColor = _alpha;
    }

    void DestroyText()
    {
        //Destroy(gameObject);
        //EffectObjectPool effectPool = (EffectObjectPool)ObjectPoolManager.instance.m_pools[(int)ObjectPoolManager.E_POOL_TYPE.EFFECT];
        EffectObjectPool effectPool = (EffectObjectPool)GameManager.instance.objectPoolManager.
            m_pools[(int)ObjectPoolManager.E_POOL_TYPE.EFFECT];
        effectPool.GetPool(EffectObjectPool.E_EFFECT_TYPE.TEXT).ReturnObject(gameObject);
    }

    private void Awake()
    {
        _tmPro = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        if(_trigger)
            ColorChange();
    }
}
