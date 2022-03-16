using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Effect_dmg : MonoBehaviour
{
    [SerializeField]
    float _moveSpeed;

    [SerializeField]
    float _alphaSpeed;

    [SerializeField]
    float _destroyTime;

    [SerializeField]
    TextMeshPro _tmPro;

    Color _alpha;

    void MoveText()
    {
        transform.Translate(new Vector3(_moveSpeed * Time.deltaTime, _moveSpeed * Time.deltaTime * -1 , 0));

        _alpha.a = Mathf.Lerp(_alpha.a, 0, _alphaSpeed * Time.deltaTime);
        _tmPro.color = _alpha;
    }

    void DestroyText()
    {
        Destroy(gameObject);
    }

    public void SetDmgText(int dmg)
    {
        _tmPro.text = dmg.ToString();
    }

    private void Awake()
    {
        _tmPro = GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        _alpha = _tmPro.color;
        //Invoke("DestroyText", _destroyTime);
    }

    private void Update()
    {
        MoveText();
    }
}
