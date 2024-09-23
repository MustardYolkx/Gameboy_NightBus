using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PassengerAnimFunc : MonoBehaviour
{

    public Passenger thisPassenger;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetPassenger(Passenger pa)
    {
        thisPassenger = pa;
    }
    public void AddToOnBusList()
    {
        GameRoot.GetInstance().GetCurrentWalkPassenger(thisPassenger);
        //GameRoot.GetInstance().OnBusPassengerOBJList(true, gameObject);
    }
    public void BeKickSFX()
    {
        MusicManager.Instance.ChangeSFXLayer2Sound(MusicManager.Instance.fly);
    }

    public void CoinSFX()
    {
        MusicManager.Instance.ChangeSFXLayer2Sound(MusicManager.Instance.Coin);
    }
    public void BeKicked()
    {
        if(thisPassenger.Type == PassengerType.HumanBeing)
        {
            //GameRoot.GetInstance().KickOffPassengerOnDriverView();
            GameRoot.GetInstance().GameOver(1);
            Destroy(gameObject);
        }
        else if (thisPassenger.Type == PassengerType.NormalGhost)
        {
            Destroy(gameObject);
            GameRoot.GetInstance().ClearNextStopPassenger();
            GameRoot.GetInstance().StartDriving();
            GameRoot.GetInstance().BusControllerPage();
        }
        else if (thisPassenger.Type == PassengerType.PowerfulGhost)
        {
            Destroy(gameObject);
            GameRoot.GetInstance().playerHP--;
            GameRoot.GetInstance().canStopAdd = false;
            
            GameRoot.GetInstance().ClearNextStopPassenger();
            GameRoot.GetInstance().StartDriving();
            GameRoot.GetInstance().BusControllerPage();
        }
        else if (thisPassenger.Type == PassengerType.SpecialGhost)
        {
            Destroy(gameObject);
            GameRoot.GetInstance().ClearNextStopPassenger();
            GameRoot.GetInstance().StartDriving();
            GameRoot.GetInstance().BusControllerPage();
        }
    }
    public void TurnOffKickButton()
    {
        GameRoot.GetInstance().TurnOffKickButton();
    }

    public void GameOverEnd2()
    {
        GameRoot.GetInstance().GameOver(2);
        Destroy(gameObject);
    }

    public void PlayCoinSound()
    {
        MusicManager.Instance.ChangeSFXLayer1Sound(MusicManager.Instance.Coin);
    }

    public void PlayStepSound()
    {
        MusicManager.Instance.ChangeSFXLayer1Sound(MusicManager.Instance.pasengerStep);
    }
}
