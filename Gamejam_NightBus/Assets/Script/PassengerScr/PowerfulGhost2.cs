using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerfulGhost2 : Passenger
{
    private string name = "Eyemon";
    private string path = "PowerfulGhost/PowerfulGhost2";
    public PowerfulGhost2() : base()
    {
        passengerName = this.name;
        prafabPath = path;
        Type = PassengerType.PowerfulGhost;
        ghostNeed = PowerfulGhostNeed.AirCondition;
    }
}
