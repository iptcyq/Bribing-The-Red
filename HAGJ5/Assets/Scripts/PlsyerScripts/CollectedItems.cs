using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectedItems : MonoBehaviour
{
    [HideInInspector]
    public string[] itemNames = new string[] 
    {
        "BrenGun",
        "Pistol",
        "Rifle",
        "Shotgun",
        "Grenade",
        "Launcher",
        "RedInfo",
        "OrangeInfo",
        "YellowInfo",
    };

    [HideInInspector]
    public bool[]  itemObtained= new bool[]
    {
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
    };

    [HideInInspector]
    public int[] rewardAmt = new int[]
    {
        1000,
        300,
        300,
        150,
        20,
        500,
        100000,
        65000,
        2500,
    };


    //things not needed
    public int noOfCoins = 0;
    public int noOfUselessStuff = 0;
    public int noOfStuff = 0;

    [Space]
    public int maxStorage = 30;
    public TextMeshProUGUI storageDisp;

    private void Start()
    {
        noOfStuff = 0;
        noOfUselessStuff = 0;
    }

    public void AddItem()
    {
        storageDisp.text = "Items Stored: " + noOfUselessStuff + "/" + maxStorage;
        noOfStuff++;
    }

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
        c += noOfCoins;
        PlayerPrefs.SetInt("noOfCoins", c);
        PlayerPrefs.SetInt("noOfUselessStuff", noOfUselessStuff);
        PlayerPrefs.SetInt("Reward", c);
        return c;
    }
}
