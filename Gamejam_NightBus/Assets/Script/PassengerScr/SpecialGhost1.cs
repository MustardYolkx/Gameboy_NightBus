using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialGhost1 : Passenger
{
    private string name = "DLDLDL";
    private string path = "SpecialGhost/SpecialGhost1";
    public SpecialGhost1() : base()
    {
        passengerName = this.name;
        prafabPath = path;
        Type = PassengerType.SpecialGhost;

    }
}
