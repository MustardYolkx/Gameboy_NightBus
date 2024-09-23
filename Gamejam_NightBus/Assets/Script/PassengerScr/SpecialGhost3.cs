using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialGhost3 : Passenger
{
    private string name = "GIGIGI";
    private string path = "SpecialGhost/SpecialGhost3";
    public SpecialGhost3() : base()
    {
        passengerName = this.name;
        prafabPath = path;
        Type = PassengerType.SpecialGhost;

    }
}
