using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerfulGhost3 : Passenger
{
    private string name = "Talkmon";
    private string path = "PowerfulGhost/PowerfulGhost3";
    public PowerfulGhost3() : base()
    {
        passengerName = this.name;
        prafabPath = path;
        Type = PassengerType.PowerfulGhost;
        ghostNeed = PowerfulGhostNeed.Talk;
    }
}
