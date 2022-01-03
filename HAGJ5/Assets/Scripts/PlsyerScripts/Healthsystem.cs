using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthsystem : MonoBehaviour
{
    public int fullHealth = 5;
    private int currentHealth;

    public Slider healthBar;
    public Color lowhealth;
    public Color highHealth;
    public Vector3 offset;

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

    public void SetHealth()
    {
        healthBar.gameObject.SetActive(currentHealth < fullHealth);

        healthBar.value = currentHealth;
        healthBar.maxValue = fullHealth;

        healthBar.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(lowhealth, highHealth, healthBar.normalizedValue);
    }

    private void Update()
    {
        healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
        SetHealth();

        if (currentHealth <= 0)
        {
            //ded
            if (team == "Player")
            {
                //they ded 
            }
            else //temp till i sort out their teams
            {
                Destroy(healthBar.gameObject);
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
