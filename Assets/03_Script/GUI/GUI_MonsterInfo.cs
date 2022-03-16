using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_MonsterInfo : MonoBehaviour
{
    [SerializeField]
    Image _img_portrait;
    [SerializeField]
    Text _txt_name;
    [SerializeField]
    Text _txt_hp;
    [SerializeField]
    Text _txt_def;
    [SerializeField]
    Text _txt_speed;
    [SerializeField]
    Transform _trs_buff_debuff_slot;
    Monster _targetMonster;

    public void SetMonsterInfo(Sprite portrait, string name, int hp, int def, float speed)
    {
        _img_portrait.sprite = portrait;
        _txt_name.text = name;
        _txt_hp.text = hp.ToString();
        _txt_def.text = def.ToString();
        _txt_speed.text = speed.ToString();
    }
    public void SetMonsterInfo(Monster target)
    {
        _targetMonster = target;

        Sprite portrait = target.GetPortrait();
        string name = target.GetName();
        int hp = target.GetMaxHP();
        int def = target.GetDEF();
        float speed = target.gameObject.GetComponent<MonsterAI>().GetSpeed();

        _img_portrait.sprite = portrait;
        _txt_name.text = name;
        _txt_hp.text = hp.ToString();
        _txt_def.text = def.ToString();
        _txt_speed.text = speed.ToString();

        UpdateBuffDebuffInfo();
    }

    void UpdateBuffDebuffInfo()
    {
        if (_targetMonster)
        {
            for (int i = 0; i < _trs_buff_debuff_slot.childCount; i++)
            {
                Destroy(_trs_buff_debuff_slot.GetChild(i).gameObject);
            }
            BuffDebuffSlot buffDebuffSlot = _targetMonster.GetBuffDebuffSlot();
            int[] buffCounts = buffDebuffSlot.GetBuffCounts();
            for (int i = 0; i < buffCounts.Length; i++)
            {
                if (buffCounts[i] > 0)
                {
                    GameObject Prefab = Resources.Load<GameObject>("Prefabs/GUI_buffCard");
                    GameObject newObj = Instantiate(Prefab, _trs_buff_debuff_slot);
                    string buffID = "buff0";
                    if (i < 10)
                        buffID += "0" + i.ToString();
                    else
                        buffID += i.ToString();
                    newObj.GetComponent<GUI_BuffCard>().SetBuffCard(buffID, buffCounts[i]);
                }
            }
        }
    }

    private void Update()
    {
        UpdateBuffDebuffInfo();
    }
}
