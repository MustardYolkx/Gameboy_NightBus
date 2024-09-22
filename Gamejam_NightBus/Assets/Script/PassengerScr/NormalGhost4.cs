using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGhost4 : Passenger
{
    private string name = "Raremon";
    private string path = "NormalGhost/NormalGhost4";
    public NormalGhost4() : base()
    {
        passengerName = this.name;
        prafabPath = path;
        Type = PassengerType.NormalGhost;
    }
}
