using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 1.5f;
    public float distance;
    public LayerMask whatisSolid;

    public GameObject destroyEffect;

    public string teamName = "Enemy";

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance, whatisSolid);
        if (hit.collider != null)
        {
            if (!hit.collider.CompareTag ("Environment")) //if didnt hit environment
            {
                //if from different teams
                if (hit.collider.GetComponent<Healthsystem>().team != teamName)
                {
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        hit.collider.GetComponent<Healthsystem>().TakeDmg(1);
                    }

                    if (hit.collider.CompareTag("Villager"))
                    {
                        hit.collider.GetComponent<Healthsystem>().TakeDmg(1);
                    }
                }
            }
            
            DestroyProjectile();
        }


        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
