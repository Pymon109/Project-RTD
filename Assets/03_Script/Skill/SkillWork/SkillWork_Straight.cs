using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWork_Straight : SkillWork
{
    [SerializeField]
    SkillEffect_Straight _straightEffect;
    public void SetStraightEffect(SkillEffect_Straight straight) { _straightEffect = straight; }

    override public void Casting()
    {
        if (_target)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/Straight/" + _straightEffect.gameObject.name);
            GameObject instance = Instantiate(prefab);
            Vector3 pos = _parent_unit.transform.position;
            pos.y = instance.transform.position.y;
            instance.transform.position = pos;
            //AreaOfEffect AOE = instance.GetComponent<AreaOfEffect>();
            instance.SetActive(false);
            SkillEffect_Straight effect = instance.GetComponent<SkillEffect_Straight>();
            effect.InitializeEffect(_target, _team, _property);
            StartCoroutine(WaitStartDelay(effect));
        }
    }

    IEnumerator WaitStartDelay(SkillEffect_Straight effect)
    {
        yield return new WaitForSeconds(effect.GetStartDelay());
        effect.gameObject.SetActive(true);
        effect.Effect();
    }

    private void Awake()
    {
        _work_type = E_WORK_TYPE.STRAIGHT;
    }

    private void Start()
    {
        _team = _parent_unit.GetTeam();
        _property = _parent_unit.GetProperty();
    }

    private void Update()
    {
        _target = _parent_unit.GetTarget();
    }
}
