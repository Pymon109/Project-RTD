using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField]
    CameraShake _camerShake;


    ////////////////////////////////////////////////////////////////////////////////이팩트 생성 기본
    public void CreateEffect(Vector3 pos, string effectPrefabName)
    {
        GameObject prefap = Resources.Load<GameObject>("Prefabs/Effect/" + effectPrefabName);
        GameObject newobj = Instantiate(prefap, pos, prefap.transform.rotation);

        newobj.GetComponent<Effect>().DestroyEffectStart();
    }

    ////////////////////////////////////////////////////////////////////////////////text effect
    [SerializeField]
    GameObject _prefap_textEffect;
    public enum E_TEXT_EFFECT_TYPE
    {
        DMG_WARTER = 0,
        DMG_FIRE,
        DMG_FOREST,
        DMG_NONE,
        DMG_PLAYER,
        TOPBAR_GOLD,
        TOPBAR_HP,
        GOLDMONSTER_DROP
    }
    public void CreateTextEffect(Vector3 pos, string text, E_TEXT_EFFECT_TYPE type)
    {
        //EffectObjectPool effectPool = (EffectObjectPool)ObjectPoolManager.instance.m_pools[(int)ObjectPoolManager.E_POOL_TYPE.EFFECT];
        EffectObjectPool effectPool = (EffectObjectPool)GameManager.instance.objectPoolManager
            .m_pools[(int)ObjectPoolManager.E_POOL_TYPE.EFFECT];
        //GameObject newobj = effectPool.GetPool(EffectObjectPool.E_EFFECT_TYPE.TEXT).GetObject(ObjectPoolManager.instance.trsDynamicObjects);
        GameObject newobj = effectPool.GetPool(EffectObjectPool.E_EFFECT_TYPE.TEXT).GetObject
            (GameManager.instance.objectPoolManager.trsDynamicObjects);
        newobj.transform.position = pos;

        Color color;
        switch (type)
        {
            case E_TEXT_EFFECT_TYPE.DMG_WARTER:
                color = new Color(0.5f, 0.75f ,1,1);
                newobj.GetComponent<TextEffect>().SetText(text, color);
                newobj.GetComponent<EffectMove>().SetMoveType(EffectMove.E_MOVE_TYPE.PROJECTILE);
                break;
            case E_TEXT_EFFECT_TYPE.DMG_FIRE:
                color = new Color(0.72f, 0.22f, 0.2f, 1);
                newobj.GetComponent<TextEffect>().SetText(text, color);
                newobj.GetComponent<EffectMove>().SetMoveType(EffectMove.E_MOVE_TYPE.PROJECTILE);
                break;
            case E_TEXT_EFFECT_TYPE.DMG_FOREST:
                color = new Color(0.36f, 0.75f, 0.42f, 1);
                newobj.GetComponent<TextEffect>().SetText(text, color);
                newobj.GetComponent<EffectMove>().SetMoveType(EffectMove.E_MOVE_TYPE.PROJECTILE);
                break;
            case E_TEXT_EFFECT_TYPE.DMG_NONE:
                color = new Color(0.59f, 0.25f, 0.56f, 1);
                newobj.GetComponent<TextEffect>().SetText(text, color);
                newobj.GetComponent<EffectMove>().SetMoveType(EffectMove.E_MOVE_TYPE.PROJECTILE);
                break;
            case E_TEXT_EFFECT_TYPE.DMG_PLAYER:
                color = new Color(0.7f, 0.18f, 0.1f, 1);
                newobj.GetComponent<TextEffect>().SetText(text, color);
                newobj.GetComponent<EffectMove>().SetMoveType(EffectMove.E_MOVE_TYPE.LINEAR);
                _camerShake.VibrateForTime(0.1f);
                break;
            case E_TEXT_EFFECT_TYPE.TOPBAR_GOLD:
                color = new Color(0.36f, 0.95f, 0.48f, 1);
                newobj.GetComponent<TextEffect>().SetText(text, color);
                newobj.GetComponent<EffectMove>().SetMoveType(EffectMove.E_MOVE_TYPE.LINEAR);
                break;
            case E_TEXT_EFFECT_TYPE.TOPBAR_HP:
                color = new Color(0.36f, 0.95f, 0.48f, 1);
                newobj.GetComponent<TextEffect>().SetText(text, color);
                newobj.GetComponent<EffectMove>().SetMoveType(EffectMove.E_MOVE_TYPE.LINEAR);
                break;
            case E_TEXT_EFFECT_TYPE.GOLDMONSTER_DROP:
                color = new Color(0.36f, 0.95f, 0.48f, 1);
                newobj.GetComponent<TextEffect>().SetText(text, color);
                newobj.GetComponent<EffectMove>().SetMoveType(EffectMove.E_MOVE_TYPE.LINEAR);
                break;
        }
    }
    public void CreateTextEffect(Vector3 pos, string text, Property property)
    {
        //EffectObjectPool effectPool = (EffectObjectPool)ObjectPoolManager.instance.m_pools[(int)ObjectPoolManager.E_POOL_TYPE.EFFECT];
        EffectObjectPool effectPool = (EffectObjectPool)GameManager.instance.objectPoolManager.
            m_pools[(int)ObjectPoolManager.E_POOL_TYPE.EFFECT];
        //GameObject newobj = effectPool.GetPool(EffectObjectPool.E_EFFECT_TYPE.TEXT).GetObject(ObjectPoolManager.instance.trsDynamicObjects);
        GameObject newobj = effectPool.GetPool(EffectObjectPool.E_EFFECT_TYPE.TEXT).GetObject(
            GameManager.instance.objectPoolManager.trsDynamicObjects);
        newobj.transform.position = pos;

        Color color;
        Property.E_PROPERTY e_property = property.GetProperty();
        switch (e_property)
        {
            case Property.E_PROPERTY.WARTER:
                color = new Color(0.5f, 0.75f, 1, 1);
                newobj.GetComponent<TextEffect>().SetText(text, color);
                newobj.GetComponent<EffectMove>().SetMoveType(EffectMove.E_MOVE_TYPE.PROJECTILE);
                break;
            case Property.E_PROPERTY.FIRE:
                color = new Color(0.72f, 0.22f, 0.2f, 1);
                newobj.GetComponent<TextEffect>().SetText(text, color);
                newobj.GetComponent<EffectMove>().SetMoveType(EffectMove.E_MOVE_TYPE.PROJECTILE);
                break;
            case Property.E_PROPERTY.FOREST:
                color = new Color(0.36f, 0.75f, 0.42f, 1);
                newobj.GetComponent<TextEffect>().SetText(text, color);
                newobj.GetComponent<EffectMove>().SetMoveType(EffectMove.E_MOVE_TYPE.PROJECTILE);
                break;
            case Property.E_PROPERTY.NONE:
                color = new Color(0.59f, 0.25f, 0.56f, 1);
                newobj.GetComponent<TextEffect>().SetText(text, color);
                newobj.GetComponent<EffectMove>().SetMoveType(EffectMove.E_MOVE_TYPE.PROJECTILE);
                break;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////goal in effect
    [SerializeField]
    GameObject _prefab_goalInEffect;
    public void CreateGoalInEffect(Vector3 pos)
    {
        //EffectObjectPool effectPool = (EffectObjectPool)ObjectPoolManager.instance.m_pools[(int)ObjectPoolManager.E_POOL_TYPE.EFFECT];
        EffectObjectPool effectPool = (EffectObjectPool)GameManager.instance.objectPoolManager.
            m_pools[(int)ObjectPoolManager.E_POOL_TYPE.EFFECT];
        ObjectPool pool = effectPool.GetPool(EffectObjectPool.E_EFFECT_TYPE.GOAL_IN);
        //GameObject newobj = pool.GetObject(ObjectPoolManager.instance.trsDynamicObjects);
        GameObject newobj = pool.GetObject(GameManager.instance.objectPoolManager.trsDynamicObjects);
        newobj.transform.position = pos;
        newobj.GetComponent<Effect>().EffectStart(pool);
    }

    ////////////////////////////////////////////////////////////////////////////////monster death effect
    [SerializeField]
    GameObject _prefab_explode;
    public void CreateMonsterDeathEffect(Vector3 pos)
    {
        //EffectObjectPool effectPool = (EffectObjectPool)ObjectPoolManager.instance.m_pools[(int)ObjectPoolManager.E_POOL_TYPE.EFFECT];
        EffectObjectPool effectPool = (EffectObjectPool)GameManager.instance.objectPoolManager.
            m_pools[(int)ObjectPoolManager.E_POOL_TYPE.EFFECT];
        ObjectPool pool = effectPool.GetPool(EffectObjectPool.E_EFFECT_TYPE.EXPLODE);
        //GameObject newobj = pool.GetObject(ObjectPoolManager.instance.trsDynamicObjects);
        GameObject newobj = pool.GetObject(GameManager.instance.objectPoolManager.trsDynamicObjects);
        newobj.transform.position = pos;
        newobj.GetComponent<Effect>().EffectStart(pool);
    }

    ////////////////////////////////////////////////////////////////////////////////skill effect
    public void CreateSkillEffect(Vector3 pos, string skillID)
    {
        //SkillEffectObjectPool skillEffectPool = (SkillEffectObjectPool)ObjectPoolManager.instance.m_pools[(int)ObjectPoolManager.E_POOL_TYPE.SKILL_EFFECT];
        SkillEffectObjectPool skillEffectPool = (SkillEffectObjectPool)GameManager.instance.objectPoolManager.
            m_pools[(int)ObjectPoolManager.E_POOL_TYPE.SKILL_EFFECT];
        ObjectPool pool = skillEffectPool.GetPool(SkillEffectObjectPool.E_FX_TYPE.SKILL, skillID);
        if (pool == null)
            return;
        //GameObject newobj = pool.GetObject(ObjectPoolManager.instance.trsDynamicObjects);
        GameObject newobj = pool.GetObject(GameManager.instance.objectPoolManager.trsDynamicObjects);
        newobj.transform.position = pos;

        newobj.GetComponent<Effect>().EffectStart(pool);
    }

    ////////////////////////////////////////////////////////////////////////////////tracking attack effect
    public void CreateTrackingAttackEffect(Vector3 pos, int unitNum, int dmg, Property property, Monster monster)
    {
        //SkillEffectObjectPool skillEffectPool = (SkillEffectObjectPool)ObjectPoolManager.instance.m_pools[(int)ObjectPoolManager.E_POOL_TYPE.SKILL_EFFECT];
        SkillEffectObjectPool skillEffectPool = (SkillEffectObjectPool)GameManager.instance.objectPoolManager.
            m_pools[(int)ObjectPoolManager.E_POOL_TYPE.SKILL_EFFECT];
        ObjectPool pool = skillEffectPool.GetPool(SkillEffectObjectPool.E_FX_TYPE.TRACKING, unitNum);
        //GameObject newobj = pool.GetObject(ObjectPoolManager.instance.trsDynamicObjects);
        GameObject newobj = pool.GetObject(GameManager.instance.objectPoolManager.trsDynamicObjects);
        newobj.transform.position = pos;
        TrackingBall tacking = newobj.GetComponent<TrackingBall>();
        tacking.Init(unitNum, dmg, property, monster);
        newobj.GetComponent<Effect>().EffectStart(pool);
    }

    public void CreateExplodeEffect(Vector3 pos, int unitNum)
    {
        //SkillEffectObjectPool skillEffectPool = (SkillEffectObjectPool)ObjectPoolManager.instance.m_pools[(int)ObjectPoolManager.E_POOL_TYPE.SKILL_EFFECT];
        SkillEffectObjectPool skillEffectPool = (SkillEffectObjectPool)GameManager.instance.objectPoolManager.
            m_pools[(int)ObjectPoolManager.E_POOL_TYPE.SKILL_EFFECT];
        ObjectPool pool = skillEffectPool.GetPool(SkillEffectObjectPool.E_FX_TYPE.TRACKING_EXPLODE, unitNum);
        //GameObject newobj = pool.GetObject(ObjectPoolManager.instance.trsDynamicObjects);
        GameObject newobj = pool.GetObject(GameManager.instance.objectPoolManager.trsDynamicObjects);
        newobj.transform.position = pos;
        newobj.GetComponent<Effect>().EffectStart(pool);
    }

    ////////////////////////////////////////////////////////////////////////////////gun attack effect
    public void CreateGunEffect(Vector3 pos, int unitNum)
    {
        //SkillEffectObjectPool skillEffectPool = (SkillEffectObjectPool)ObjectPoolManager.instance.m_pools[(int)ObjectPoolManager.E_POOL_TYPE.SKILL_EFFECT];
        SkillEffectObjectPool skillEffectPool = (SkillEffectObjectPool)GameManager.instance.objectPoolManager.
            m_pools[(int)ObjectPoolManager.E_POOL_TYPE.SKILL_EFFECT];
        ObjectPool pool = skillEffectPool.GetPool(SkillEffectObjectPool.E_FX_TYPE.GUN, unitNum);
        //GameObject newobj = pool.GetObject(ObjectPoolManager.instance.trsDynamicObjects);
        GameObject newobj = pool.GetObject(GameManager.instance.objectPoolManager.trsDynamicObjects);
        newobj.GetComponent<Effect>().EffectStart(pool);
    }

    ////////////////////////////////////////////////////////////////////////////////gold effect
    public void CreateGoldEffect(Vector3 pos)
    {
        //EffectObjectPool effectPool = (EffectObjectPool)ObjectPoolManager.instance.m_pools[(int)ObjectPoolManager.E_POOL_TYPE.EFFECT];
        EffectObjectPool effectPool = (EffectObjectPool)GameManager.instance.objectPoolManager.
            m_pools[(int)ObjectPoolManager.E_POOL_TYPE.EFFECT];
        ObjectPool pool = effectPool.GetPool(EffectObjectPool.E_EFFECT_TYPE.GOLD);
        //GameObject newobj = pool.GetObject(ObjectPoolManager.instance.trsDynamicObjects);
        GameObject newobj = pool.GetObject(GameManager.instance.objectPoolManager.trsDynamicObjects);
        newobj.transform.position = pos;
        newobj.GetComponent<Effect>().EffectStart(pool);
    }
}
