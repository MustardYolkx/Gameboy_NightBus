using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human2 : Passenger
{
    private string name = "Gay";
    private string path = "HumanBeing/Human2";
    public Human2() : base()
    {
        passengerName = this.name;
        prafabPath = path;
        Type = PassengerType.HumanBeing;
    }
}
