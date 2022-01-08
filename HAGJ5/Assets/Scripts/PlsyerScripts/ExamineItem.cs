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

    //examine item
    public Image imageDisp; 
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI desctext;
    public GameObject popUp; //holding all of the above

    public LayerMask whatIsPick;
    private Item emptyItem;
    public GameObject empty;


    // Start is called before the first frame update
    void Start()
    {
        emptyItem = empty.GetComponent<Item>();

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
            imageDisp.sprite = emptyItem.itemImg;
            titleText.text = emptyItem.dispName;
            desctext.text = emptyItem.description + " \n" + "Value of item: " + emptyItem.value;

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
    }

    private void PickUp(GameObject obj)
    {
        if (obj.tag == "item" && !holding)
        {
            //can be picked
            FindObjectOfType<AudioManager>().Play("PickUp");
            holding = true;

            itemHolding = obj;
            itemHolding.SetActive(false);

            Item itemPicked = itemHolding.GetComponent<Item>();

            imageDisp.sprite = itemPicked.itemImg;
            titleText.text = itemPicked.dispName;
            desctext.text = itemPicked.description + " \n" + "Value of item: " + itemPicked.value;
        }
    }

}
