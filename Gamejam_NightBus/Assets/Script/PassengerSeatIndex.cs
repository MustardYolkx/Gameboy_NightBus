using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerSeatIndex : MonoBehaviour
{
    public int turnOnBus;
    public int seatIndex;
    public Passenger thisPassenger;

    public int maxTurn;
    public int minTurn;

    public int targetTurn;
    // Start is called before the first frame update
    void Start()
    {
        SetMaxTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DetermineMaxMinTurn()
    {
        if (thisPassenger.Type == PassengerType.HumanBeing)
        {
            minTurn = 2;
            maxTurn = 3;
        }
    }
    public void SetMaxTurn()
    {
        targetTurn = Random.Range(minTurn, maxTurn);

    }
    public void InputSeatIndex(int seat, Passenger pa)
    {
        seatIndex= seat;
        thisPassenger = pa;
    }

    public void BeKickedOut()
    {
        Destroy(gameObject);
    }
    public void NormalGhostKicked()
    {

    }

    public void IsTurnToGetOff()
    {
        if(thisPassenger.Type == PassengerType.HumanBeing)
        {
            if(turnOnBus == targetTurn)
            {
                GameRoot.GetInstance().OnBusPassengerDic(false, seatIndex, thisPassenger);
                GameRoot.GetInstance().OnBusPassengerOBJList(false, seatIndex, this.gameObject);
            }
        }
        else if(thisPassenger.Type == PassengerType.NormalGhost)
        {

        }
        else
        {

        }
    }
}
