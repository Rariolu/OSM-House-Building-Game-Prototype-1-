using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseViewSingle //: NullableInstanceClassSingleton<HouseViewSingle>
{
    House currentHouse;
    public House CurrentHouse
    {
        get
        {
            return currentHouse;
        }
    }
    HouseViewSingle(House house)
    {
        currentHouse = house;
    }
    public static void ShowHouse(House house)
    {
        SingletonUtil.SetInstance(new HouseViewSingle(house));
    }
}