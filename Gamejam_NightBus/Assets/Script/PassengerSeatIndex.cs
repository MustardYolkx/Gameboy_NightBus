using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

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
        StartCoroutine(StartDelay());
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(0.3f);
         TurnOnBusNormalMusic();
        DetermineMaxMinTurn();
        SetMaxTurn();
        if (thisPassenger.Type == PassengerType.PowerfulGhost)
        {
            StartCoroutine(DelayCheck());
        }
    }
    IEnumerator DelayCheck()
    {
        yield return new WaitForSeconds(0.2f);
        
        GhostNeedCheck();
        StartCoroutine(DelayCheck());

    }
    private void OnEnable()
    {
        GetComponent<Animator>().SetTrigger(currentAnimState);
    }

    public void TurnOnBusNormalMusic()
    {
        if(thisPassenger.ghostNeed ==PowerfulGhostNeed.Music)
        {
            GameRoot.GetInstance().normalMusic= true;
        }
    }

    public void TurnOffBusNormalMusic()
    {
        if (thisPassenger.ghostNeed == PowerfulGhostNeed.Music)
        {
            GameRoot.GetInstance().normalMusic = false;
        }
    }
    public void DetermineMaxMinTurn()
    {
        if (thisPassenger.Type == PassengerType.HumanBeing)
        {
            minTurn = 2;
            maxTurn = 4;
        }
        else if(thisPassenger.Type == PassengerType.PowerfulGhost)
        {
            minTurn = 2;
            maxTurn = 4;
        }
        else if(thisPassenger.Type == PassengerType.SpecialGhost)
        {
            minTurn = 2;
            maxTurn = 4;
        }
    }
    public void SetMaxTurn()
    {
        targetTurn = UnityEngine.Random.Range(minTurn, maxTurn);

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

    public void BeKickSFX()
    {
        MusicManager.Instance.ChangeSFXLayer2Sound(MusicManager.Instance.fly);
    }
    public void BeKickedAnimFun()
    {
         if (thisPassenger.Type == PassengerType.HumanBeing)
         {
            GameRoot.GetInstance().canSit[seatIndex] = true;
            GameRoot.GetInstance().GameOver(1);
             Destroy(gameObject);
         }
         else if (thisPassenger.Type == PassengerType.NormalGhost)
         {
            GameRoot.GetInstance().canSit[seatIndex] = true;
            Destroy(gameObject);
         }
        else if (thisPassenger.Type == PassengerType.PowerfulGhost)
        {
            if (turnOnBus != 0)
            {
                GameRoot.GetInstance().stopIndex-=turnOnBus;
            }
            else
            {
                
            }
            TurnOffBusNormalMusic();
            GameRoot.GetInstance().canSit[seatIndex] = true;
            GameRoot.GetInstance().playerHP--;
            GameRoot.GetInstance().canStopAdd = false;
            Destroy(gameObject);
        }
         else if(thisPassenger.Type == PassengerType.SpecialGhost)
         {
            GameRoot.GetInstance().canSit[seatIndex] = true;
            Destroy(gameObject);
         }
    }
    public void NormalGhostKicked()
    {

    }

    public void GetOffAnimFunc()
    {
        if(thisPassenger.Type == PassengerType.HumanBeing)
        {
            GameRoot.GetInstance().canSit[seatIndex] = true;
            //GameRoot.GetInstance().PassengerGetOffSideView();
            //GameRoot.GetInstance().OnBusPassengerDic(false, seatIndex, thisPassenger);
            //GameRoot.GetInstance().OnBusPassengerOBJList(false, seatIndex, this.gameObject);
        }
        else if (thisPassenger.Type == PassengerType.PowerfulGhost)
        {
            TurnOffBusNormalMusic();
            GameRoot.GetInstance().canSit[seatIndex] = true;
            GameRoot.GetInstance().PassengerGetOffSideView();
            GameRoot.GetInstance().OnBusPassengerDic(false, seatIndex, thisPassenger);
            GameRoot.GetInstance().OnBusPassengerOBJList(false, seatIndex, this.gameObject);
        }
        
    }

    IEnumerator GetOffProcess()
    {
        if(thisPassenger.Type == PassengerType.HumanBeing)
        {
            while (Vector2.Distance(transform.position, GameRoot.GetInstance().passengerGetOffPos.position) > 0.2f)
            {
                transform.position = Vector2.MoveTowards(transform.position, GameRoot.GetInstance().passengerGetOffPos.position, Time.deltaTime * 3);
                yield return null;
            }
            GameRoot.GetInstance().PassengerGetOffSideView();
            GameRoot.GetInstance().OnBusPassengerDic(false, seatIndex, thisPassenger);
            GameRoot.GetInstance().OnBusPassengerOBJList(false, seatIndex, this.gameObject);
        }
        else if(thisPassenger.Type == PassengerType.SpecialGhost)
        {
            while (Vector2.Distance(transform.position, GameRoot.GetInstance().driverPos.position) > 0.2f)
            {
                transform.position = Vector2.MoveTowards(transform.position, GameRoot.GetInstance().driverPos.position, Time.deltaTime * 6);
                yield return null;
            }
            GameRoot.GetInstance().GameOver(2);
            Destroy(gameObject);
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
                GameRoot.GetInstance().canSit[seatIndex] = true;
                StartCoroutine(GetOffProcess());
                
            }
        }
        else if(thisPassenger.Type == PassengerType.NormalGhost)
        {
            GameRoot.GetInstance().canSit[seatIndex] = true;
        }
        else if (thisPassenger.Type == PassengerType.PowerfulGhost)
        {
            turnOnBus++;
            if (isNeedMeet)
            {
                TurnOffBusNormalMusic();
                GetComponent<Animator>().SetTrigger("GetOff");
                GameRoot.GetInstance().canSit[seatIndex] = true;
                //GameRoot.GetInstance().OnBusPassengerDic(false, seatIndex, thisPassenger);
                //GameRoot.GetInstance().OnBusPassengerOBJList(false, seatIndex, this.gameObject);
            }
            else
            {
                if (turnOnBus == targetTurn)
                {
                    TurnOffBusNormalMusic();
                    GameRoot.GetInstance().canSit[seatIndex] = true;
                    GameRoot.GetInstance().playerHP--;
                    GameRoot.GetInstance().OnBusPassengerDic(false, seatIndex, thisPassenger);
                    GameRoot.GetInstance().OnBusPassengerOBJList(false, seatIndex, this.gameObject);
                }
                else
                {

                }
            }
            
        }
        else if(thisPassenger.Type == PassengerType.SpecialGhost)
        {
            turnOnBus++;
            if (turnOnBus == targetTurn)
            {
                StartCoroutine(GetOffProcess());
                GameRoot.GetInstance().canSit[seatIndex] = true;
                GetComponent<Animator>().SetTrigger("GetOffGhost");
            }
            else
            {
                
                GetComponent<Animator>().SetTrigger("BeGhost");
                GameRoot.GetInstance().BusGhostLight();
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
        if (thisPassenger.ghostNeed == PowerfulGhostNeed.Talk)
        {
            if (turnOnBus == targetTurn)
            {
                GameRoot.GetInstance().OnBusPassengerDic(false, seatIndex, thisPassenger);
                GameRoot.GetInstance().OnBusPassengerOBJList(false, seatIndex, this.gameObject);
            }
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
