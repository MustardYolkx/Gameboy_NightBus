using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGhost0 : Passenger
{
    private string name = "Raremon";
    private string path = "NormalGhost/NormalGhost0";
    public NormalGhost0() : base()
    {
        passengerName = this.name;
        prafabPath = path;
        Type = PassengerType.NormalGhost;
    }
}
