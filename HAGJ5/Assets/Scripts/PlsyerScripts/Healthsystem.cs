using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthsystem : MonoBehaviour
{
    public int fullHealth = 5;
    private int currentHealth;

    public GameObject deathEffect;

    public string team;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = fullHealth;
    }

    public void TakeDmg(int dmg)
    {
        currentHealth -= dmg;
    }

    public void GainHealth(int health)
    {
        if ((currentHealth + health) < fullHealth)
        {
            currentHealth += health;
        }
        else if (currentHealth <= fullHealth && (currentHealth + health) > fullHealth)
        {
            currentHealth = fullHealth;
        }
        else
        {
            Debug.Log("Alr max health");
        }
    }

    private void Update()
    {
        if (currentHealth < 0)
        {
            //ded
            if (team == "Player")
            {
                //they ded 
            }
            else //temp till i sort out their teams
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
