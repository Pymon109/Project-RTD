using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    float _shakeForce;
    float _shakeTime;
    Vector3 _initialPos;

    //////////////////////////////////////////////test
    public float testTime;

    private void Start()
    {
        _initialPos = transform.position;
    }

    public void VibrateForTime(float time)

    {
        _shakeTime = time;
    }

    private void Update()
    {
        if (_shakeTime > 0)
        {
            transform.position = Random.insideUnitSphere * _shakeForce + _initialPos;
            _shakeTime -= Time.deltaTime;
        }
        else
        {
            _shakeTime = 0.0f;
            transform.position = _initialPos;
        }
    }
}
