using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //attack
    public GameObject projectile;
    public Transform firePoint;

    private float currentTimeBtwShots;
    public float timeBtwShots;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        if (Input.GetMouseButtonDown(0) && currentTimeBtwShots <=  0)
        {
            GameObject bullet = Instantiate(projectile, firePoint.position, transform.rotation);
            bullet.GetComponent<BulletScript>().teamName = "Player";
            currentTimeBtwShots = timeBtwShots;
        }
        else
        {
            currentTimeBtwShots -= Time.deltaTime;
        }
    }
}
