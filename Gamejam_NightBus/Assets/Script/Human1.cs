using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Human1 : Passenger
{
    private string name = "Jack";
    private string path = "HumanBeing/Human1";
    public Human1() : base()
    {
        passengerName = this.name;
        prafabPath = path;
        Type = PassengerType.HumanBeing;
    }

    
}
