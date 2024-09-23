using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialGhost : Passenger
{
    private string name = "GAHAHA";
    private string path = "SpecialGhost/SpecialGhost0";
    public SpecialGhost() : base()
    {
        passengerName = this.name;
        prafabPath = path;
        Type = PassengerType.SpecialGhost;
        
    }
}
