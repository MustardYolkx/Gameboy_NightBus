using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioSource bgm;
    public AudioSource SFXsoundLayer1;
    public AudioSource SFXsoundLayer2;
    #region BGM
    public AudioClip backgroundMusic1;
    #endregion


    #region SFX - 1
    public AudioClip busDoorOpen;
    public AudioClip Coin;
    public AudioClip normalButtonChange, passengerChooseChange;
    public AudioClip normalButtonPress, kickButtonPress;

    public AudioClip BeKicked, fly;

    public AudioClip getOnBus;
    public AudioClip pasengerStep;
    #endregion

    #region SFX - 2
    public AudioClip busEngine;
    public AudioClip busStop;
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
        BGM1();
    }
    public void BGMAudioPlay()
    {
        bgm.Play();
    }
    public void BGM1()
    {
        bgm.clip= backgroundMusic1;
        BGMAudioPlay();
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
    public void BusDoorOpen()
    {
        SFXsoundLayer1.clip = busDoorOpen;
    }

    public void CoinSound()
    {
        SFXsoundLayer1.clip = busDoorOpen;
    }
}
