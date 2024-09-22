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
    public string currentAnimState;

    public bool isNeedMeet;

    
    // Start is called before the first frame update
    void Start()
    {
        DetermineMaxMinTurn();
        SetMaxTurn();
    }

    // Update is called once per frame
    void Update()
    {
        GhostNeedCheck();
    }
    private void OnEnable()
    {
        GetComponent<Animator>().SetTrigger(currentAnimState);
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
        
         GetComponent<Animator>().SetBool("BeKicked",true);
        
        
    }
    public void BeKickedAnimFun()
    {
         if (thisPassenger.Type == PassengerType.HumanBeing)
         {
             GameRoot.GetInstance().GameOver(1);
             Destroy(gameObject);
         }
         else if (thisPassenger.Type == PassengerType.NormalGhost)
         {
             Destroy(gameObject);
         }
        else
        {
            GameRoot.GetInstance().canStopAdd = false;
            Destroy(gameObject);
        }
    }
    public void NormalGhostKicked()
    {

    }

    public void GetOffAnimFunc()
    {
        if(thisPassenger.Type!= PassengerType.HumanBeing)
        {

            GameRoot.GetInstance().PassengerGetOffSideView();
            GameRoot.GetInstance().OnBusPassengerDic(false, seatIndex, thisPassenger);
            GameRoot.GetInstance().OnBusPassengerOBJList(false, seatIndex, this.gameObject);
        }
        else if (thisPassenger.Type != PassengerType.PowerfulGhost)
        {
            GameRoot.GetInstance().OnBusPassengerDic(false, seatIndex, thisPassenger);
            GameRoot.GetInstance().OnBusPassengerOBJList(false, seatIndex, this.gameObject);
        }
    }
    public void IsTurnToGetOff()
    {
        
        if(thisPassenger.Type == PassengerType.HumanBeing)
        {
            turnOnBus++;
            if (turnOnBus == targetTurn)
            {
                GetComponent<Animator>().SetTrigger("GetOff");
                
            }
        }
        else if(thisPassenger.Type == PassengerType.NormalGhost)
        {

        }
        else
        {
            if(isNeedMeet)
            {
                GameRoot.GetInstance().OnBusPassengerDic(false, seatIndex, thisPassenger);
                GameRoot.GetInstance().OnBusPassengerOBJList(false, seatIndex, this.gameObject);
            }
            else
            {

            }
        }
    }
    public void GhostNeedCheck()
    {
        if(thisPassenger.ghostNeed== PowerfulGhostNeed.Music)
        {
            isNeedMeet = GameRoot.GetInstance().musicOn;
        }
        if(thisPassenger.ghostNeed == PowerfulGhostNeed.AirCondition)
        {
            isNeedMeet = GameRoot.GetInstance().airConditionOn;
        }
        if(thisPassenger.ghostNeed == PowerfulGhostNeed.Horn)
        {
            isNeedMeet = GameRoot.GetInstance().hornOn;
        }
    }

}
public enum PowerfulGhostNeed
{
    None,
    Music,
    AirCondition,
    Horn,
    Talk,
}
