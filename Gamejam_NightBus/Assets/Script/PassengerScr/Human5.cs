using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human5 : Passenger
{
    private string name = "Alice";
    private string path = "HumanBeing/Human5";
    public Human5() : base()
    {
        passengerName = this.name;
        prafabPath = path;
        Type = PassengerType.HumanBeing;
    }
}
