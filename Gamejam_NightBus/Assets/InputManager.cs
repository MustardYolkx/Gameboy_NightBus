using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{

    public static InputManager Instance;

    private PlayerControl playerControl;
    public PlayerControl.BusControllerActions busControllerActions;

    public PlayerControl.GameStartPhaseActions gameStartPhaseActions;
    //private PlayerInput playerInput;

    public GameObject busControllerMenu;
    public GameObject busControllerMenuFirst;

    public GameObject driverViewMenu;
    public GameObject driverViewMenuFirst;
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        playerControl = new PlayerControl();
        busControllerActions = playerControl.BusController;
        gameStartPhaseActions = playerControl.GameStartPhase;
        gameStartPhaseActions.Enable();
        //busControllerActions.OpenBusController.Enable();
        //busControllerActions.DriverView.Enable();
        //playerInput= GetComponent<PlayerInput>();
    }
    // Start is called before the first frame update
    void Start()
    {
        AddInputActionCallBacks();
        StopMouseInputStart();
    }

    // Update is called once per frame
    void Update()
    {
        StopMouseInput();
    }
    public void StopMouseInputStart()
    {
        // 隐藏并锁定鼠标光标
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        
        
    }
    public void StopMouseInput()
    {
        if (Cursor.visible || Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }

        public void AddInputActionCallBacks()
    {
        busControllerActions.OpenBusController.started += OpenBusControlller;
        busControllerActions.DriverView.started += OnDriverView;
        busControllerActions.ConfirmButton.started += OnConfirmButtonPress;
        

        gameStartPhaseActions.Start.started += OnGameStart;
    }


    public void AddCancelButtonCallBack()
    {
        busControllerActions.CancelButton.started += OnCancelButtonPress;
    }
    public void RemoveCancelButtonCallBack()
    {
        busControllerActions.CancelButton.started -= OnCancelButtonPress;
    }

    public void RemoveInputActionCallBacks()
    {
        busControllerActions.OpenBusController.started -= OpenBusControlller;
        busControllerActions.DriverView.started -= OnDriverView;
        busControllerActions.ConfirmButton.started -= OnConfirmButtonPress;
        busControllerActions.CancelButton.started -= OnCancelButtonPress;
        gameStartPhaseActions.Start.started -= OnGameStart;
    }

    #region Input Func
    private void OnGameStart(InputAction.CallbackContext obj)
    {
        GameRoot.GetInstance().GameStart();
        
    }
    private void OnCancelButtonPress(InputAction.CallbackContext obj)
    {
        GameRoot.GetInstance().KickOutSubMenuClose();
        RemoveCancelButtonCallBack();
    }

    private void OnConfirmButtonPress(InputAction.CallbackContext obj)
    {
        PressButton();
    }
    public void PressButton()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            // Try to get the Button component from the selected game object
            Button selectedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

            if (selectedButton != null)
            {
                // We have a selected button!
                Debug.Log("Currently selected button: " + selectedButton.name);

                // Here you can do something with the selected button
                // For example, if you want to "click" it when a certain key is pressed:
                
                    selectedButton.onClick.Invoke();
                
            }
        }
    }
    #endregion
    #region Menu Open and Close
    private void OpenBusControlller(InputAction.CallbackContext obj)
    {
        busControllerMenu.SetActive(true);
        driverViewMenu.SetActive(false);
        GameRoot.GetInstance().ClearNextStopPassenger();
        EventSystem.current.SetSelectedGameObject(busControllerMenuFirst);
    }

    private void OnDriverView(InputAction.CallbackContext obj)
    {
        driverViewMenu.SetActive(true);
        busControllerMenu.SetActive(false);
        GameRoot.GetInstance().GeneratePassengerOnStop(false, PassengerType.HumanBeing);
        EventSystem.current.SetSelectedGameObject(driverViewMenuFirst);
    }
    #endregion

}
