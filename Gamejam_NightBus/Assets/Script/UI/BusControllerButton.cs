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
        });

        hornButton.onClick.AddListener(() =>
        {
            HornButtonFunc();
        });

        kickButton.onClick.AddListener(() =>
        {
            KickButtonFunc();
        });

        musicButton.onClick.AddListener(() =>
        {
            MusicButtonFunc();
        });
    }

    private void MusicButtonFunc()
    {

        GameRoot.GetInstance().MusicOn();
    }

    private void KickButtonFunc()
    {
        GameRoot.GetInstance().KickOutSubMenu();
        GameRoot.GetInstance().DisableBusControlPage();
    }

    private void HornButtonFunc()
    {
        throw new NotImplementedException();
    }

    private void AirConditionButtonFunc()
    {
        GameRoot.GetInstance().AirConditionOn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
