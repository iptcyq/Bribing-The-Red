using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilitaryScript : MonoBehaviour
{
    public bool advanced = false;

    //spot player
    public float visionRange = 5;
    public LayerMask whatIsVisible;

    public float startWaitTime;
    private float waitTime;

    private bool facingRight = true;

    //follow
    public float speed = 4f;
    public Transform target;
    public float attackDist = 3;

    private RaycastHit2D hit;

    //attack
    public GameObject projectile;
    public float timeBtwshots = 0.8f;
    private float nextShotTime;

    private void Start()
    {
        startWaitTime = Random.Range(1f, 3f);
        waitTime = startWaitTime;
    }


    // Update is called once per frame
    void Update()
    {
        if (!facingRight)
        {
            hit = Physics2D.Raycast(transform.position, -transform.right, visionRange, whatIsVisible);
        }
        else
        {
            hit = Physics2D.Raycast(transform.position, transform.right, visionRange, whatIsVisible);
        }

        if (hit.collider != null)
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);

            //spots player
            if (hit.collider.tag == "Player")
            {
                waitTime = startWaitTime;

                if (Vector2.Distance(transform.position, target.position)> attackDist)
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                }
                else
                {
                    if (Time.time > nextShotTime)
                    {
                        Vector3 difference = target.position - transform.position;
                        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90f;

                        GameObject bullet = Instantiate(projectile, transform.position, Quaternion.Euler(0f, 0f, rotZ));
                        bullet.GetComponent<BulletScript>().teamName = "Enemy";
                        nextShotTime = Time.time + timeBtwshots;
                    }
                }
            }

        }
        else
        {
            if (!facingRight)
            {
                Debug.DrawLine(transform.position, transform.position - transform.right * visionRange, Color.green);
            }
            else
            {
                Debug.DrawLine(transform.position, transform.position + transform.right * visionRange, Color.green);
            }
        }

        if (waitTime <= 0)
        {
            facingRight = !facingRight;

            if (advanced)
            {
                startWaitTime = Random.Range(1f, 3f);
            }

            waitTime = startWaitTime;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
