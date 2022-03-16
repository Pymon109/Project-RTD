using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static SoundManager _unique;
    public static SoundManager _instance { get { return _unique; } }

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

    private void Awake()
    {
        _unique = this;
    }

    private void Start()
    {
        _audio_BGM.Stop();
    }
}
