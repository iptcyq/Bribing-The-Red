using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilitaryScript : MonoBehaviour
{
    public bool advanced = false;

    //looks
    private bool facingRight = true;
    public Animator anim;

    //spot player
    public float visionRange = 5;
    public LayerMask whatIsVisible;

    public float startWaitTime;
    private float waitTime;

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
        startWaitTime = Random.Range(2f, 4f);
        waitTime = startWaitTime;
        
        anim.SetBool("isRunning", false);
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
                    anim.SetBool("isRunning", true);
                    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                }
                else
                {
                    anim.SetBool("isRunning", false);

                    if (Time.time > nextShotTime)
                    {
                        Vector3 difference = target.position - transform.position;
                        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

                        GameObject bullet = Instantiate(projectile, transform.position, Quaternion.Euler(0f, 0f, rotZ));
                        bullet.GetComponent<BulletScript>().teamName = "Enemy";
                        nextShotTime = Time.time + timeBtwshots;
                    }
                }
            }

        }
        else
        {
            anim.SetBool("isRunning", false);

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
            Flip();

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

        void Flip()
        {
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }
    }


}
