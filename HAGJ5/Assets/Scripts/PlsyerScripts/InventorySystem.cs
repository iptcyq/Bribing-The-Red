using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public GameObject[] items;
    public int maxStorage = 5;
    private int currentStorage = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentStorage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "item")
        {
            if (currentStorage < maxStorage)
            {
                //can pick up

                currentStorage++;
            }
        }
    }
}
