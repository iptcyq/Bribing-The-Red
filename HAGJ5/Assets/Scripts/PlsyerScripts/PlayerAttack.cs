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

    private SpriteRenderer sr;
    private Sprite storeSprite;
    private bool holdingGun = false;

    public GameObject cursor;

    // Start is called before the first frame update
    void Start()
    {
        holdingGun = false;
        sr = GetComponentInChildren<SpriteRenderer>();
        storeSprite = sr.sprite;
        sr.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPoint = (Vector3)Input.mousePosition;
        screenPoint.z = 10f;
        cursor.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            holdingGun = !holdingGun;
            if (holdingGun) { sr.sprite = storeSprite; }
            else { sr.sprite = null; }
        }

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        if (Input.GetMouseButtonDown(0) && currentTimeBtwShots <=  0 && holdingGun)
        {
            FindObjectOfType<AudioManager>().Play("Shoot");
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
