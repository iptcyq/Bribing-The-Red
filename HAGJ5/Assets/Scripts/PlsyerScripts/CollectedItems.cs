using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedItems : MonoBehaviour
{
    public string[] itemNames = new string[] 
    {
        "BrenGun",
        "ChinPengAlive",
        "ChinPengDead",
        "Pistol",
        "ChinPengInfo",
        "Politburo",
        "Members",
    };

    public bool[]  itemObtained= new bool[]
    {
        false,
        false,
        false,
        false,
        false,
        false,
        false,
    };

    public int[] rewardAmt = new int[]
    {
        1000,
        2500000,
        125000,
        1000,
        100000,
        65000,
        2500,
    };


    //things not needed
    public int noOfUselessStuff = 0;

    //calculate total amount of rewards at the end
    public int TabulateRewards()
    {
        int c = 0;
        for(int i =0; i<itemObtained.Length -1; i++)
        {
            if (itemObtained[i] == true)
            {
                c += rewardAmt[i];
            }
        }
        PlayerPrefs.SetInt("Reward", c);
        return c;
    }
}
