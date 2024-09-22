using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGhost3 : Passenger
{
    private string name = "Raremon";
    private string path = "NormalGhost/NormalGhost3";
    public NormalGhost3() : base()
    {
        passengerName = this.name;
        prafabPath = path;
        Type = PassengerType.NormalGhost;
    }
}
