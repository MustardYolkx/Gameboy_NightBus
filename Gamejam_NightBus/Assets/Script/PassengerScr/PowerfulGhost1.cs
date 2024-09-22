using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerfulGhost1 : Passenger
{
    private string name = "Eyemon";
    private string path = "PowerfulGhost/PowerfulGhost1";
    public PowerfulGhost1() : base()
    {
        passengerName = this.name;
        prafabPath = path;
        Type = PassengerType.PowerfulGhost;
        ghostNeed = PowerfulGhostNeed.Horn;
    }
}
