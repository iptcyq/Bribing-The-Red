using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExamineItem : MonoBehaviour
{
    private bool holding = false;
    private bool looking = false;

    private Item itemPicked;
    private GameObject itemHolding;

    public Image imageDisp; //display to show the item
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI desctext;
    public GameObject popUp; //holding all of the above

    public LayerMask whatIsPick;

    // Start is called before the first frame update
    void Start()
    {
        holding = false;
        looking = false;

        popUp.SetActive(false);
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
            PickUp(result.gameObject);
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
    }

    private void PickUp(GameObject obj)
    {
        if (obj.tag == "item" && !holding)
        {
            //can be picked
            holding = true;

            itemHolding = obj;
            itemHolding.SetActive(false);

            itemPicked = obj.GetComponent<Item>();

            imageDisp.sprite = itemPicked.itemImg;
            titleText.text = itemPicked.dispName;
            desctext.text = itemPicked.description + " \n" + "Value of item: " + itemPicked.value + "   AUTHORISED: " + itemPicked.authorisedToBeHeld;
            
            looking = true;
            popUp.gameObject.SetActive(true);
        }
    }

}
