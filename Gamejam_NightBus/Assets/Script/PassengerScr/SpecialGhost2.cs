using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialGhost2 : Passenger
{
    private string name = "LULI";
    private string path = "SpecialGhost/SpecialGhost2";
    public SpecialGhost2() : base()
    {
        passengerName = this.name;
        prafabPath = path;
        Type = PassengerType.SpecialGhost;

    }
}

