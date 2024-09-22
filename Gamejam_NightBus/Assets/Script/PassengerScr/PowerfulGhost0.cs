using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerfulGhost0 : Passenger
{
    private string name = "Eyemon";
    private string path = "PowerfulGhost/PowerfulGhost0";
    public PowerfulGhost0() : base()
    {
        passengerName = this.name;
        prafabPath = path;
        Type = PassengerType.PowerfulGhost;
        ghostNeed = PowerfulGhostNeed.Music;
    }
}
