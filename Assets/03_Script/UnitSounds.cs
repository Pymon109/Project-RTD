using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSounds : MonoBehaviour
{
    [SerializeField]
    List<AudioSource> _list_audio_NormalAttack;
    [SerializeField]
    AudioSource _audio_create;

    public void PlayNormalAttackAudio(Unit.E_ATTACK_TYPE type)
    {
        _list_audio_NormalAttack[(int)type].Play();
    }

    private void Start()
    {
        for(int i = 0; i < _list_audio_NormalAttack.Count; i++)
        {
            _list_audio_NormalAttack[i].volume = 0.25f;
            _list_audio_NormalAttack[i].Stop();
        }
        _audio_create.volume = 0.5f;
    }
}
