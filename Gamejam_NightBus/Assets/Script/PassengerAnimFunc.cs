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

    public void TurnOffKickButton()
    {
        GameRoot.GetInstance().TurnOffKickButton();
    }

    public void GameOverEnd2()
    {
        GameRoot.GetInstance().GameOver(2);
        Destroy(gameObject);
    }
}
