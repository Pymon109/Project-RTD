using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMove : MonoBehaviour
{
    public float _xScale;
    public float _yScale;
    public float _upScale;
    public float _upSpeed;

    float y;

    Vector3 sunrise;
    Vector3 sunset;
    public float reduceHeight;

    Vector3 upPos;
    public float journeyTime;
    float startTime;

    public enum E_MOVE_TYPE
    {
        PROJECTILE = 0,
        LINEAR
    }
    public E_MOVE_TYPE _moveType;

    public void SetMoveType(E_MOVE_TYPE type)
    {
        float a, z;
        switch (type)
        {
            case E_MOVE_TYPE.PROJECTILE:
                sunrise = transform.position;
                y = transform.position.y + (_yScale * -1.0f);
                a = 17.5f / 48.2f;
                z = (y - 17.5f) / a;
                sunset = new Vector3(transform.position.x + _xScale, y, z);
                break;
            case E_MOVE_TYPE.LINEAR:
                y = transform.position.y + _upScale;
                a = 17.5f / 48.2f;
                z = (y - 17.5f) / a;
                upPos = new Vector3(transform.position.x, y, z);
                break;
        }
        _moveType = type;
        startTime = Time.time;
    }

        void Projectile()
    {
        Vector3 center = (sunrise + sunset) * 0.5F;
        center -= new Vector3(0, 1 * reduceHeight, 0);
        Vector3 riseRelCenter = sunrise - center;
        Vector3 setRelCenter = sunset - center;
        float fracComplete = (Time.time - startTime) / journeyTime;
        Vector3 newPos = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        //newPos.z = (newPos.y - 17.5f) / (17.5f / 48.2f);
        transform.position = newPos;
        transform.position += center;
    }

    void LinearMotion()
    {
        Vector3 vDist = upPos - transform.position;
        Vector3 vDir = vDist.normalized;
        float fDist = vDist.magnitude;
        float fracComplete = (Time.time - startTime) / journeyTime;
        if (fDist > Time.deltaTime)
        {
            transform.position += vDir * fracComplete;
        }
    }

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        switch (_moveType)
        {
            case E_MOVE_TYPE.PROJECTILE:
                Projectile();
                break;
            case E_MOVE_TYPE.LINEAR:
                LinearMotion();
                break;
            default:
                break;
        }
    }
}
