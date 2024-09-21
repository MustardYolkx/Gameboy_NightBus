using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human0 : Passenger
{
    private string name = "Alice";
    private string path = "HumanBeing/Human0";
    public Human0() : base()
    {
        passengerName = this.name;
        prafabPath = path;
        Type = PassengerType.HumanBeing;
    }
}
