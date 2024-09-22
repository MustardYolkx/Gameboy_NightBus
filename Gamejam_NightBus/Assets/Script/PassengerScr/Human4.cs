using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human4 : Passenger
{
    private string name = "Alice";
    private string path = "HumanBeing/Human4";
    public Human4() : base()
    {
        passengerName = this.name;
        prafabPath = path;
        Type = PassengerType.HumanBeing;
    }
}
