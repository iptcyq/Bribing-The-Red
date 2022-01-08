using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Zones : MonoBehaviour
{
    public bool win = false;
    public CollectedItems ci;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (win)
        {
            //load winning scene
            if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                SceneManager.LoadScene("Real");
            }
            else if (SceneManager.GetActiveScene().name == "Real")
            {
                ci.TabulateRewards();
                SceneManager.LoadScene("Leaderboard");
            }
        }
        else
        {
            if (other.tag == "Player")
            {
                //end and reload I guess
                FindObjectOfType<AudioManager>().Play("Dead");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
