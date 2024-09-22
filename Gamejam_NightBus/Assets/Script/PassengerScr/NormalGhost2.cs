using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGhost2 : Passenger
{
    private string name = "Raremon";
    private string path = "NormalGhost/NormalGhost2";
    public NormalGhost2() : base()
    {
        passengerName = this.name;
        prafabPath = path;
        Type = PassengerType.NormalGhost;
    }
}
