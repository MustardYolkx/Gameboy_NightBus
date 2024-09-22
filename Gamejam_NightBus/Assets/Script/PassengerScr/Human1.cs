using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human1 : Passenger
{
    private string name = "Man";
    private string path = "HumanBeing/Human1";
    public Human1() : base()
    {
        passengerName = this.name;
        prafabPath = path;
        Type = PassengerType.HumanBeing;
    }
}
