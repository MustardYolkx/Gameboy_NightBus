using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DriverViewButton : MonoBehaviour
{

    public Button kickButton;

    public Button steeringWheel;
    // Start is called before the first frame update
    void Start()
    {
        
        kickButton.onClick.AddListener(() =>
        {
            KickButtonFunc();
        });

        steeringWheel.onClick.AddListener(() =>
        {
            SteeringWheelFunc();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KickButtonFunc()
    {
        //TODO:
        //Kick the passenger (a function on Passenger Scr
        //Play Animation

        //Turn to Bus side view
        GameRoot.GetInstance().KickOffPassengerOnDriverView();
        GameRoot.GetInstance().BusControllerPage();
        GameRoot.GetInstance().StartDriving();
    }

    public void SteeringWheelFunc()
    {
        //TODO:        
        //Play Animation
        //Turn to Bus side view
        GameRoot.GetInstance().BusControllerPage();
        GameRoot.GetInstance().StartDriving();
    }
}
