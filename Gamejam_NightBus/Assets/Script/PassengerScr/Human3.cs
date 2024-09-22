using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human3 : Passenger
{
    private string name = "Alice";
    private string path = "HumanBeing/Human3";
    public Human3() : base()
    {
        passengerName = this.name;
        prafabPath = path;
        Type = PassengerType.HumanBeing;
    }
}
