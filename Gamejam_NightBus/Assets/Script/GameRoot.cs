using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static PlayerControl;

public class GameRoot : MonoBehaviour
{

    private static GameRoot instance;

    public Passenger_Dictionary Passenger_Dic { get; private set; }
    public static GameRoot GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("Can't get Gameroot");
            return instance;
        }
        return instance;
    }

    #region Lists
    public Dictionary<int,Passenger> OnBusPassenger_Dic { get; private set; }

    public List<Passenger> NextStopPassenger_List { get; private set; }

    public List<GameObject> NextStopPassOBJ_List { get; private set; }
    public Dictionary<int, GameObject> OnBusPassOBJ_Dic { get; private set; }

    public List<Transform> busSeatPos;
    public List<bool> canSit;
    public Transform GetOnPos;
    public Transform WalkToSeatPos;

    public GameObject onBusPassenger;
    public Passenger currentWalkPassenger;
    public bool passengerGetOn;

    public List<Passenger> AppearedPowerfulGhost { get; private set; }
    #endregion

    #region UI Menu
   
    public GameObject busControllerMenu;
    public GameObject busControllerMenuFirst;

    public GameObject driverViewMenu;
    public GameObject driverViewMenuFirst;

    public GameObject startPage;

    public Button kickButtonInDriverView;

    public GameObject AC_Light;
    public GameObject Music_Light;
    public GameObject Kick_Light;
    public GameObject Horn_Light;

    public GameObject passengerChoseMenu;
    public List<Button> passengerChoseList;

    public GameObject end1;
    public GameObject end2;
    
    #endregion

    #region Game Loop
    public int stopIndex = 0;
    #endregion
    #region Environment Bool
    public bool airConditionOn = false;
    public bool musicOn = false;
    #endregion
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
        Passenger_Dic = new Passenger_Dictionary();
        OnBusPassenger_Dic = new Dictionary<int, Passenger>();
        OnBusPassOBJ_Dic = new Dictionary<int, GameObject>();
        NextStopPassenger_List = new List<Passenger>();
        NextStopPassOBJ_List = new List<GameObject>();
        AppearedPowerfulGhost = new List<Passenger>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(Passenger_Dic.passengers[0].prafabPath);

        //Debug.Log(SerchForHumanbeing()[0].passengerName);

        //SerchForType(PassengerType.HumanBeing);
        
        startPage.SetActive(true);
        busControllerMenu.SetActive(false);
        driverViewMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    #region setting

    #endregion
    #region Generate Passenger List

    public List<Passenger> SerchForType(PassengerType passengerType)
    {
        List<Passenger> typePassenger = new List<Passenger>();
        foreach (Passenger p in Passenger_Dic.passengers.Values)
        {
            if (p.Type == passengerType)
            {
                typePassenger.Add(p);

            }
        }
        return typePassenger;
    }
    public List<Passenger> SerchForHumanbeing()
    {
        List<Passenger> humanbeings = new List<Passenger>();
        foreach(Passenger p in Passenger_Dic.passengers.Values)
        {
            if(p.Type == PassengerType.HumanBeing)
            {
                humanbeings.Add(p);
                
            }
        }
        return humanbeings;
    }

    public List<Passenger> SerchForNormalGhost()
    {
        List<Passenger> normalGhost = new List<Passenger>();
        foreach (Passenger p in Passenger_Dic.passengers.Values)
        {
            if (p.Type == PassengerType.NormalGhost)
            {
                normalGhost.Add(p);
            }
        }
        return normalGhost;
    }

    public List<Passenger> SerchForPowerfulGhost()
    {
        List<Passenger> powerfulGhost = new List<Passenger>();
        foreach (Passenger p in Passenger_Dic.passengers.Values)
        {
            if (p.Type == PassengerType.PowerfulGhost)
            {
                powerfulGhost.Add(p);
            }
        }
        return powerfulGhost;
    }

    public void OnBusPassengerDic(bool add, int i ,Passenger p)
    {
        if (add)
        {
            OnBusPassenger_Dic.Add(i,p);

        }
        else
        {
            OnBusPassenger_Dic.Remove(i);
        }
    }
    public void OnBusPassengerOBJList(bool add,int i, GameObject p)
    {
        if (add)
        {
            OnBusPassOBJ_Dic.Add(i,p);

        }
        else
        {
            Destroy(OnBusPassOBJ_Dic[i]);
            OnBusPassOBJ_Dic.Remove(i);
        }
    }

    public void ClearOnBusPassengerDic()
    {
        OnBusPassenger_Dic.Clear();
        foreach (GameObject obj in NextStopPassOBJ_List)
        {
            Destroy(obj);
        }
        OnBusPassOBJ_Dic.Clear();
    }
    public void ResetAllTheSeat()
    {
        for(int i =0;i<canSit.Count;i++)
        {
            canSit[i] = true;
        }
    }
    public void NextStopPassengerList(bool add, Passenger p)
    {
        if (add)
        {
            NextStopPassenger_List.Add(p);

        }
        else
        {
            NextStopPassenger_List.Remove(p);
        }
    }

    public void ClearNextStopPassenger()
    {
        NextStopPassenger_List.Clear();
        foreach(GameObject obj in NextStopPassOBJ_List)
        {
            Destroy(obj);
            
        }
        NextStopPassOBJ_List.Clear();
    }
    #endregion

    #region Main Methods
    public void GetCurrentWalkPassenger(Passenger p)
    {
        currentWalkPassenger= p;
        passengerGetOn = true;
    }
    public void GenerateSideViewPassenger(Passenger p)
    {
        GameObject pa = Instantiate(onBusPassenger, WalkToSeatPos.position,Quaternion.identity);
        
        for(int i = 0; i < 50; i++)
        {
            int index = UnityEngine.Random.Range(0, canSit.Count);
            if (canSit[index])
            {
                OnBusPassengerDic(true, index, p);
                OnBusPassengerOBJList(true,index,pa);
                if(pa.GetComponent<PassengerSeatIndex>() != null)
                {
                    pa.GetComponent<PassengerSeatIndex>().InputSeatIndex(index,p);
                }
                StartCoroutine(PassengerWalk(pa, index));
                canSit[index] = false;
                break;
            }
            
        }
          
    }

    IEnumerator PassengerWalk(GameObject pa, int index)
    {
        while (Vector2.Distance(pa.transform.position, busSeatPos[index].position) > 0.2f)
        {
            
            pa.transform.position = Vector2.MoveTowards(pa.transform.position, busSeatPos[index].position, Time.deltaTime * 2);
            yield return null;
        }
        if(pa.GetComponent<Animator>() != null)
        {
            pa.GetComponent<Animator>().SetTrigger("Sit");
            pa.GetComponent<PassengerSeatIndex>().currentAnimState = "Sit";
        }
    }
    public void GeneratePassengerOnStop(bool randomType,PassengerType type)
    {
        //TODO:
        //Two Mode: Random or controlled
        //Generate powerful ghost/normal ghost/human
        if(randomType)
        {
            
            
                       
        }
        else if (type == PassengerType.PowerfulGhost)
        {
            for (int c = 0; c < 30; c++)
            {
                if (SerchForType(type).Count > 0)
                {
                    int i = UnityEngine.Random.Range(0, SerchForType(type).Count);
                    if (AppearedPowerfulGhost.Contains(SerchForType(type)[i]))
                    {
                        continue;
                    }
                    else
                    {
                        AppearedPowerfulGhost.Add(SerchForType(type)[i]);
                        GameObject pa = Resources.Load<GameObject>(SerchForType(type)[i].prafabPath);
                        NextStopPassengerList(true, SerchForType(type)[i]);
                        GameObject passenger = Instantiate(pa, GetOnPos.position, Quaternion.identity);
                        if (passenger.GetComponent<PassengerAnimFunc>() != null)
                        {
                            passenger.GetComponent<PassengerAnimFunc>().SetPassenger(SerchForType(type)[i]);
                        }
                        NextStopPassOBJ_List.Add(passenger);
                        break;
                    }
                }

            }

        }
        else
        {
            List<Passenger> passes = SerchForType(type);
            int i = UnityEngine.Random.Range(0, passes.Count);
            GameObject pa = Resources.Load<GameObject>(passes[i].prafabPath);
            NextStopPassengerList(true, passes[i]);
            GameObject passenger = Instantiate(pa, GetOnPos.position, Quaternion.identity);
            if(passenger.GetComponent<PassengerAnimFunc>() != null)
            {
                passenger.GetComponent<PassengerAnimFunc>().SetPassenger(passes[i]);
            }
            NextStopPassOBJ_List.Add(passenger);
        }
                
    }
    public void KickOffPassengerWhenBusDriving(int i)
    {
        if (OnBusPassOBJ_Dic.ContainsKey(i))
        {
            OnBusPassOBJ_Dic[i].GetComponent<PassengerSeatIndex>().BeKickedOut();
            OnBusPassenger_Dic.Remove(i);
            OnBusPassOBJ_Dic.Remove(i);
            KickOutSubMenuClose();
        }
        
              
    }

    public void PlayPassengerKickAni()
    {
        NextStopPassOBJ_List[0].GetComponent<Animator>().SetTrigger("BeKicked");
    }
    public void KickOffPassengerOnDriverView()
    {
        
        ClearNextStopPassenger();
        currentWalkPassenger= null;

        BusControllerPage();
        StartDriving();

    }


    private void CheckAllPassengerState()
    {
        if(OnBusPassOBJ_Dic.Count> 0)
        {
            for (int i = 0; i < canSit.Count; i++)
            {
                if (OnBusPassOBJ_Dic.ContainsKey(i))
                {
                    OnBusPassOBJ_Dic[i].GetComponent<PassengerSeatIndex>().IsTurnToGetOff();
                }
                
            }
        }
        
                        
    }
    #endregion
    public void SwitchViewToDriver()
    {
        //TODO:
        //Pop out the driver view
        //Enable the button in driver view
        //Disable the button on side bus view
    }

    public void SwitchViewToBus() 
    {
        //TODO:
        //Close the driver view 
        //Enable the bus controller
        //Disable the driver view button
    }

    #region ChangeBusMenu

    public void StartGamePageOff()
    {
        startPage.SetActive(false);
        InputManager.Instance.gameStartPhaseActions.Disable();
    }
    public void BusControllerPage()
    {
        busControllerMenu.SetActive(true);
        driverViewMenu.SetActive(false);
        ButtonLightState();
        TurnOnPassengerOnDriverView();
        if (NextStopPassenger_List.Count > 0)
        {
            if (currentWalkPassenger != null)
            {

                GenerateSideViewPassenger(currentWalkPassenger);
            }
        }
        
        GetInstance().ClearNextStopPassenger();
        
        EventSystem.current.SetSelectedGameObject(busControllerMenuFirst);
    }

    public void DriverViewPage()
    {

        driverViewMenu.SetActive(true);
        busControllerMenu.SetActive(false);
        
        TurnOnKickButton();
        TurnOffPassengerOnDriverView();
        //InputManager.Instance.busControllerActions.DriverView.Enable();
        stopIndex++;
        CheckAllPassengerState();
        EventSystem.current.SetSelectedGameObject(driverViewMenuFirst);
    }

    

    public void TurnOffPassengerOnDriverView()
    {
        if(OnBusPassOBJ_Dic.Count>0)
        {
            foreach (GameObject ob in OnBusPassOBJ_Dic.Values)
            {
                ob.SetActive(false);
            }
        }
        
    }

    public void TurnOnPassengerOnDriverView()
    {
        if (OnBusPassOBJ_Dic.Count > 0)
        {
            foreach (GameObject ob in OnBusPassOBJ_Dic.Values)
            {
                ob.SetActive(true);
            }
        }
    }
    public void KickOutSubMenu()
    {
        passengerChoseMenu.SetActive(true);
        
        InputManager.Instance.AddCancelButtonCallBack();
        Navigation nav = new Navigation();
        nav.mode = Navigation.Mode.Explicit;
        for(int i= 0; i< passengerChoseList.Count;i++)
        {
            if (OnBusPassOBJ_Dic.ContainsKey(i))
            {
                passengerChoseList[i].gameObject.SetActive(true);
                for(int c = 0; c < passengerChoseList.Count; c++)
                {
                    if (c < i)
                    {
                        if (OnBusPassOBJ_Dic.ContainsKey(c))
                        {

                            nav.selectOnRight = passengerChoseList[c];
                        }
                    }
                    else if (c > i)
                    {
                        if (OnBusPassOBJ_Dic.ContainsKey(c))
                        {

                            nav.selectOnLeft = passengerChoseList[c];
                        }
                    }  
                }
                passengerChoseList[i].navigation = nav;
            }
            else
            {
                passengerChoseList[i].gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < passengerChoseList.Count; i++)
        {
            if (passengerChoseList[i].gameObject.activeSelf)
            {
                EventSystem.current.SetSelectedGameObject(passengerChoseList[i].gameObject);
                //EventSystem.current.SetSelectedGameObject(passengerChoseList[1].gameObject);
                break;
            }
        }

        }
    public void KickOutSubMenuClose()
    {
       
        for (int i = 0; i<passengerChoseList.Count;i++)
        {
            passengerChoseList[i].gameObject.SetActive(false);
            
        }
        passengerChoseMenu.SetActive(false);
        EnableBusControlPage();
    }
    public void DisableBusControlPage()
    {
        busControllerMenu.GetComponent<CanvasGroup>().interactable = false;    
    }
    public void EnableBusControlPage()
    {
        busControllerMenu.GetComponent<CanvasGroup>().interactable = true;
        EventSystem.current.SetSelectedGameObject(busControllerMenuFirst);
    }
    public void TurnOffKickButton()
    {
        kickButtonInDriverView.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(driverViewMenuFirst);
    }
    public void TurnOnKickButton()
    {
        kickButtonInDriverView.gameObject.SetActive(true);
    }

    public void GenerateUIPage(GameObject ob)
    {
        Canvas ca = FindObjectOfType<Canvas>();
        Instantiate(ob, ca.transform);
    }
    #endregion

    #region Bus Environment Bool
    public void ButtonLightState()
    {
        AC_Light.GetComponent<Animator>().SetBool("LightOn", airConditionOn);
        Music_Light.GetComponent<Animator>().SetBool("LightOn", musicOn);
    }
    public void AirConditionOn()
    {
        airConditionOn = true;
    }

    public void AirConditionOff()
    {
        airConditionOn= false;
    }

    public void MusicOn()
    {
        musicOn= true;
    }

    public void MusicOff()
    {
        musicOn= false;
    }

    public void HornPressed()
    {

    }
    #endregion
    public void GameOver(int i)
    {
        //There are 4 ends in this game
        //TODO:
        //1st step
        //Player complete the game, happy ending

        //Player was complained by the company, game over
        if (i == 1)
        {
            GenerateUIPage(end1);
            Button b = end1.GetComponentInChildren<Button>();
            b.onClick.AddListener(() => ReStart());
            EventSystem.current.SetSelectedGameObject(b.gameObject);
        }
        //Player killed by the ghost, game over
        else if (i==2)
        {
            GenerateUIPage(end2);
            Button b = end2.GetComponentInChildren<Button>();
            b.onClick.AddListener(()=>ReStart());
            EventSystem.current.SetSelectedGameObject(b.gameObject);
        }
        //Player drive to the ghost stop, game over

        //2nd step
        //Play end animation

        //3rd step
        //Reset all the data
        //Jump to the start page
    }
    public void ReStart()
    {
        //ResetAllUI();
        //ResetData();
        SceneManager.LoadScene("GameScene");
    }

    public void ResetAllUI()
    {
        startPage.SetActive(true);
        busControllerMenu.SetActive(false);
        driverViewMenu.SetActive(false);
        
        passengerChoseMenu.SetActive(false);
        
    }
    public void ResetData()
    {
        ClearNextStopPassenger();
        ClearOnBusPassengerDic();
        ResetAllTheSeat();
    }
    public void GameStart()
    {
        //TODO:
        //Play start animation
        StartCoroutine(StepOne());
        //Set default data
    }

    IEnumerator StepOne()
    {
        StartGamePageOff();
        BusControllerPage();
        yield return new WaitForSeconds(1);
        InputManager.Instance.busControllerActions.Enable();
        DriverViewPage();
        GenerateArrangement();
    }

    public void StartDriving()
    {
        StartCoroutine(DrivingProcess());
    }
    IEnumerator DrivingProcess()
    {
        yield return new WaitForSeconds(8);
        DriverViewPage();
        GenerateArrangement();
    }

    public void AddTurnOnStop()
    {
        for(int i =0;i<canSit.Count;i++)
        {
            if(OnBusPassOBJ_Dic.ContainsKey(i))
            {
                OnBusPassOBJ_Dic[i].GetComponent<PassengerSeatIndex>().turnOnBus++;
            }
        }
    }

    public void GenerateArrangement()
    {
        List<int> humanStop = new List<int> { 1,2,6,9,11,13,16,17,19,20 };
        List<int> normalGhostStop = new List<int> { 3, 4, 7,10,14 ,18};
        List<int> powerfulGhostStop = new List<int> { 5, 8, 12, 15 };

        if(humanStop.Contains(stopIndex))
        {
            GeneratePassengerOnStop(false, PassengerType.HumanBeing);
        }
        else if(normalGhostStop.Contains(stopIndex))
        {
            GeneratePassengerOnStop(false, PassengerType.NormalGhost);
        }
        else
        {
            GeneratePassengerOnStop(false, PassengerType.PowerfulGhost);
        }
               
    }
}
