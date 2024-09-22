using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger_Dictionary 
{
    public Dictionary<int, Passenger> passengers ;

    public static Passenger_Dictionary instance;
    public static Passenger_Dictionary GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("Can't get Gameroot");
            return instance;
        }
        return instance;
    }
    //public void Initialize()
    //{

    //    passengers.Add(0,new Human1());
        
    //}

    public Passenger_Dictionary()
    {
        passengers = new Dictionary<int, Passenger>
        {
            {0,new Human0() },
            {1,new NormalGhost0()},
            {2,new PowerfulGhost0()},
            //{1,new Human1() }
        };
    }
}
