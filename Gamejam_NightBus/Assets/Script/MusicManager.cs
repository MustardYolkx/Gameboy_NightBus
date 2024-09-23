using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioSource bgm;
    public AudioSource bgm2;
    public AudioSource SFXsoundLayer1;
    public AudioSource SFXsoundLayer2;
    public AudioSource SFXsoundLayer3;
    #region BGM
    public AudioClip backgroundMusic1;
    public AudioClip end1;
    public AudioClip end2;
    public AudioClip end3;
    public AudioClip end4;
    #endregion


    #region SFX - 1
    public AudioClip busDoorOpen;
    public AudioClip Coin;
    

    public AudioClip BeKicked, fly;

    public AudioClip getOnBus;
    public AudioClip pasengerStep;

    public AudioClip normalMusic, badMusic;

    public AudioClip hornSFX;
    #endregion

    #region SFX - 2
    public AudioClip busEngine;
    public AudioClip busStop;

    #endregion

    #region SFX - 3
    public AudioClip normalButtonChange, passengerChooseChange;
    public AudioClip normalButtonPress, kickButtonPress;
    #endregion
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
    }
    void Start()
    {
        GameStartBGM();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BGMStop()
    {
        bgm.Stop();
    }
    public void GameStartBGM()
    {
        ChangeBGM1(backgroundMusic1);
    }
    public void BGM1AudioPlay()
    {
        bgm.Play();
    }

    public void BGM2AudioPlay()
    {
        bgm2.Play();
    }
    public void ChangeBGM1(AudioClip sound)
    {
        bgm.clip= sound;
        BGM1AudioPlay();
    }
    public void ChangeBGM2(AudioClip sound)
    {
        bgm2.Stop();
        bgm2.clip = sound;
        BGM2AudioPlay();
    }
    public void ChangeSFXLayer1Sound(AudioClip sound)
    {
        SFXsoundLayer1.Stop();
        SFXsoundLayer1.clip = sound;
        SFXsoundLayer1.Play();
    }
    public void ChangeSFXLayer2Sound(AudioClip sound)
    {
        SFXsoundLayer2.Stop();
        SFXsoundLayer2.clip = sound;
        SFXsoundLayer2.Play();
    }

    public void ChangeSFXLayer3Sound(AudioClip sound)
    {
        SFXsoundLayer3.Stop();
        SFXsoundLayer3.clip = sound;
        SFXsoundLayer3.Play();
    }
    public void BusDoorOpen()
    {
        SFXsoundLayer1.clip = busDoorOpen;
    }

    public void CoinSound()
    {
        SFXsoundLayer1.clip = busDoorOpen;
    }
}
