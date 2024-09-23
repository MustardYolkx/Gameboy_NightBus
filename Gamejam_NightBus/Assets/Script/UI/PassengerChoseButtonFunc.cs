using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PassengerChoseButtonFunc : MonoBehaviour
{

    public Button seat0;
    public Button seat1;    
    public Button seat2;
    public Button seat3;
    public Button seat4;

    // Start is called before the first frame update
    void Start()
    {
        seat0.onClick.AddListener(() =>
        {
            Seat0ButtonFunc();
            TurnOffControllerButton();
        });

        seat1.onClick.AddListener(() =>
        {
            Seat1ButtonFunc();
            TurnOffControllerButton();
        });
        seat2.onClick.AddListener(() =>
        {
            Seat2ButtonFunc();
            TurnOffControllerButton();
        });
        seat3.onClick.AddListener(() =>
        {
            Seat3ButtonFunc();
            TurnOffControllerButton();
        });
        seat4.onClick.AddListener(() =>
        {
            Seat4ButtonFunc();
            TurnOffControllerButton();
        });
    }
    private void TurnOffControllerButton()
    {
        InputManager.Instance.RemoveConfirmButtonCallBack();
        GameRoot.GetInstance().TurnOffControllerButton();
        
    }
    private void Seat0ButtonFunc()
    {
        GameRoot.GetInstance().KickOffPassengerWhenBusDriving(0);
        
    }

    private void Seat1ButtonFunc()
    {
        GameRoot.GetInstance().KickOffPassengerWhenBusDriving(1);
    }

    private void Seat2ButtonFunc()
    {
        GameRoot.GetInstance().KickOffPassengerWhenBusDriving(2);
    }

    private void Seat3ButtonFunc()
    {
        GameRoot.GetInstance().KickOffPassengerWhenBusDriving(3);
    }

    private void Seat4ButtonFunc()
    {
        GameRoot.GetInstance().KickOffPassengerWhenBusDriving(4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
