using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BusControllerButton : MonoBehaviour
{

    public Button airConditionButton;
    public Button hornButton;
    public Button kickButton;
    public Button musicButton;

    
    // Start is called before the first frame update
    void Start()
    {
        airConditionButton.onClick.AddListener(() =>
        {
            AirConditionButtonFunc();
            TurnOffButton();
        });

        hornButton.onClick.AddListener(() =>
        {
            HornButtonFunc();
            TurnOffButton();
        });

        kickButton.onClick.AddListener(() =>
        {
            KickButtonFunc();
        });

        musicButton.onClick.AddListener(() =>
        {
            MusicButtonFunc();
            TurnOffButton();
        });
    }
    public void TurnOffButton()
    {
        GameRoot.GetInstance().TurnOffBusControlButton();
        InputManager.Instance.busControllerActions.ConfirmButton.Disable();
    }
    private void MusicButtonFunc()
    {

        if (GameRoot.GetInstance().musicOn)
        {
            GameRoot.GetInstance().MusicOff();
            MusicManager.Instance.bgm2.Stop();
        }
        else
        {
            GameRoot.GetInstance().MusicOn();
            if (GameRoot.GetInstance().normalMusic)
            {
                MusicManager.Instance.ChangeBGM2(MusicManager.Instance.normalMusic);
            }
            else
            {
                MusicManager.Instance.ChangeBGM2(MusicManager.Instance.badMusic);
            }
        }
        GameRoot.GetInstance().ButtonLightState();
    }

    private void KickButtonFunc()
    {
        MusicManager.Instance.ChangeSFXLayer3Sound(MusicManager.Instance.normalButtonPress);
        GameRoot.GetInstance().KickOutSubMenu();
        GameRoot.GetInstance().DisableBusControlPage();
    }

    private void HornButtonFunc()
    {
        MusicManager.Instance.ChangeSFXLayer3Sound(MusicManager.Instance.normalButtonPress);
        MusicManager.Instance.ChangeSFXLayer1Sound(MusicManager.Instance.hornSFX);
        GameRoot.GetInstance().HornOn();
        
    }

    private void AirConditionButtonFunc()
    {
        MusicManager.Instance.ChangeSFXLayer3Sound(MusicManager.Instance.normalButtonPress);
        if (GameRoot.GetInstance().airConditionOn)
        {
            GameRoot.GetInstance().AirConditionOff();
            
        }
        else
        {
            GameRoot.GetInstance().AirConditionOn();
        }
        GameRoot.GetInstance().ButtonLightState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
