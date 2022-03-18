using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource _audio_BGM;

    [SerializeField]
    List<AudioSource> _list_audio_NormalAttack;


    public void BGMStart()
    {
        _audio_BGM.volume = 0.25f;
        _audio_BGM.loop = true;
        _audio_BGM.Play();
    }

    private void Start()
    {
        _audio_BGM.Stop();
    }
}
