using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExamineItem : MonoBehaviour
{
    //check if holding anything
    private bool holding = false;
    private bool looking = false;

    //currently holding
    private GameObject itemHolding;
    public SpriteRenderer showHolding;

    //examine item
    public Image imageDisp; 
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI desctext;
    public GameObject popUp; //holding all of the above

    public LayerMask whatIsPick;

    //inventory
    public GameObject[] whatIsHeld;
    public int maxStorage = 5;

    public int currentitem = 0;


    // Start is called before the first frame update
    void Start()
    {
        holding = false;
        looking = false;

        popUp.SetActive(false);

        currentitem = 0;
        SelectItem();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && holding) //drop item
        {
            if (looking) { looking = false; }

            itemHolding.transform.position = transform.position;
            itemHolding.SetActive(true);
            
            itemHolding = null;
            holding = false;
        }
        else if (Input.GetKeyDown(KeyCode.E) && !holding) //pick up item
        {
            Collider2D result = Physics2D.OverlapCircle(transform.position, 1f, whatIsPick);

            if (result != null)
            {
                PickUp(result.gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.F)) //examine
        {
            looking = !looking;

            if (looking)
            {
                //to look at the thing
                popUp.gameObject.SetActive(true);
            }
            else
            {
                popUp.gameObject.SetActive(false);
            }
        }

        //scroll through to hold items
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            //solidfying the item in storage
            if (itemHolding == null)
            {
                whatIsHeld[currentitem] = null;
            }
            else
            {
                itemHolding = whatIsHeld[currentitem];
            }

            //scrolling to new item
            if (currentitem >= maxStorage-1)
            {
                currentitem = 0;
            }
            else
            {
                currentitem++;
            }
            
            SelectItem();
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (itemHolding == null)
            {
                whatIsHeld[currentitem] = null;
            }
            else
            {
                itemHolding = whatIsHeld[currentitem];
            }

            if (currentitem <= 0)
            {
                currentitem = maxStorage -1;
            }
            else
            {
                currentitem--;
            }
            
            SelectItem();
        }
        
    }

    private void PickUp(GameObject obj)
    {
        if (obj.tag == "item" && !holding)
        {
            //can be picked
            holding = true;

            itemHolding = obj;
            itemHolding.SetActive(false);

            Item itemPicked = itemHolding.GetComponent<Item>();

            imageDisp.sprite = itemPicked.itemImg;
            titleText.text = itemPicked.dispName;
            desctext.text = itemPicked.description + " \n" + "Value of item: " + itemPicked.value + "   AUTHORISED: " + itemPicked.authorisedToBeHeld;
        }
    }

    void SelectItem()
    {
        int i = 0;
        foreach (GameObject item in whatIsHeld)
        {
            if (i == currentitem)
            {
                if (itemHolding == null)
                {
                    showHolding.sprite = null;
                }
                else
                {
                    //display item
                    showHolding.sprite = itemHolding.GetComponent<Item>().itemImg;
                }
            }
            i++;
        }
    }

}
