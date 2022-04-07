using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgSoundSource;
    public GameObject bgOn;
    public GameObject bgOff;

    public void SetMusicVolume(float volume)
    {
        bgSoundSource.volume = volume;
    }

    public void SoundOff()
    {
        bgSoundSource.mute = true;
        bgOn.SetActive(false);
        bgOff.SetActive(true);
    }
    public void SoundOn()
    {
        bgSoundSource.mute = false;
        bgOff.SetActive(false);
        bgOn.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        bgOn.SetActive(true);
        bgOff.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
