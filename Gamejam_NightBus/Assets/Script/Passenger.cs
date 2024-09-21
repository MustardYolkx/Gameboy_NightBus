using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger
{
 
    public string passengerName;
    public int id;
    public PassengerType Type;
    public string prafabPath;
    public Passenger()
    {

    }

    public void GetInfo(int id, string prafabPath, PassengerType type)
    {
        this.id = id;
        //passengerName = name;
    }
}

public enum PassengerType
{
    HumanBeing,
    NormalGhost,
    PowerfulGhost,
}
