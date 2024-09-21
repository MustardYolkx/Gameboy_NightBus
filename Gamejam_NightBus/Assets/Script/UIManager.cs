using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static PlayerControl;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Stack<GameObject> UIMenu;

    private Canvas canvas;

    public GameObject busControllerMenu;
    public GameObject busControllerMenuFirst;

    public GameObject driverViewMenu;
    public GameObject driverViewMenuFirst;

    public GameObject startPage;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        canvas = FindObjectOfType<Canvas>();
        //busControllerActions.OpenBusController.Enable();
        //busControllerActions.DriverView.Enable();
        //playerInput= GetComponent<PlayerInput>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushIn(GameObject obj)
    {
        if(!UIMenu.Contains(obj))
        {
            Instantiate(obj, canvas.transform);
            EventSystem.current.SetSelectedGameObject(obj.GetComponentInChildren<Button>().gameObject);
            UIMenu.Push(obj);
        }
        
    }

    public void PopOut()
    {

    }
}
