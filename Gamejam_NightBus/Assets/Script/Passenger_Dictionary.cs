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
            {3,new Human1() },
            {4,new Human2() },
            {5,new Human3() },
            {6,new Human4() },
            {7,new Human5() },
            {8,new NormalGhost1()},
            {9,new NormalGhost2()},
            {10,new NormalGhost3()},
            {11,new NormalGhost4()},
            {12,new PowerfulGhost1()},
            {13,new PowerfulGhost2()},
            {14,new PowerfulGhost3()},
            //{1,new Human1() }
        };
    }
}
