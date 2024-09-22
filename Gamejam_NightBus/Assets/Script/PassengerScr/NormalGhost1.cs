using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGhost1 : Passenger
{
    private string name = "Raremon";
    private string path = "NormalGhost/NormalGhost1";
    public NormalGhost1() : base()
    {
        passengerName = this.name;
        prafabPath = path;
        Type = PassengerType.NormalGhost;
    }
}
