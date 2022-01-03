using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerScript : MonoBehaviour
{
    public bool hostile = false; //only hostile after events happen

    public float speed = 1;
    public float RoamX;
    public float RoamY;

    private Vector2 randomSpot;
    private float spot;

    public float startWaitTime;
    private float waitTime;

    public float visionRange = 4f;
    public LayerMask whatIsVisible;

    private RaycastHit2D hit;

    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        spot = Random.Range(RoamX, RoamY);
        randomSpot = new Vector2(spot, transform.position.y);

        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, randomSpot, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, randomSpot) < 0.2f)
        {
            if (waitTime <= 0)
            {
                spot = Random.Range(RoamX, RoamY);
                randomSpot = new Vector2(spot, transform.position.y);

                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        //check if player in range, check if hostile
        if (facingRight && spot < transform.position.x)
        {
            //flip to facing left
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;

            facingRight = !facingRight;
        }
        else if (!facingRight && spot > transform.position.x)
        {
            //flip to facing right
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;

            facingRight = !facingRight;
        }




        if (facingRight)
        {
            hit = Physics2D.Raycast(transform.position, transform.right, visionRange, whatIsVisible);
        }
        else if (!facingRight)
        {
            hit = Physics2D.Raycast(transform.position, -transform.right, visionRange, whatIsVisible);
        }


        if (hit.collider != null)
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);

            if (hit.collider.tag == "Player" && hostile == true)
            {
                //evade the player a lot more than just the guards
                if (facingRight) //player on the right
                {
                    spot = Random.Range(RoamX, RoamY);
                    spot = ((RoamY - RoamX) * (0.4f)) + RoamX;
                }
                else
                {
                    spot = Random.Range(RoamX, RoamY);
                    spot = (RoamY - (RoamY - RoamX) * (0.4f));
                }

                randomSpot = new Vector2(spot, transform.position.y);

                waitTime = 0f;
                
            }
            else if (hit.collider.tag == "Enemy")
            {
                //evade the militaty slightly
                spot = Random.Range(RoamX, RoamY);
                spot =  ((RoamY - RoamX) * (0.7f)) + RoamX;

                randomSpot = new Vector2(spot, transform.position.y);


                waitTime = 0f;

            }
        }
        else
        {
            if (facingRight)
            {
                Debug.DrawLine(transform.position, transform.position + transform.right * visionRange, Color.green);
            }
            else
            {
                Debug.DrawLine(transform.position, transform.position -transform.right * visionRange, Color.green);
            }
        }

    }
}
