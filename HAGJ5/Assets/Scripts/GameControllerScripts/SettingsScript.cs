using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    
    private float currentVolume;

    public GameObject settingsMenu;
    private bool menuOut = false;

    // Start is called before the first frame update
    void Start()
    {
        currentVolume = PlayerPrefs.GetFloat("volume", 0f);
        audioMixer.SetFloat("volume", currentVolume);
        volumeSlider.value = currentVolume;
        menuOut = false;
        settingsMenu.SetActive(false);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuOut = !menuOut;

            settingsMenu.SetActive(menuOut);
        }
    }

    public void Settingscene()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuOut = !menuOut;

            settingsMenu.SetActive(menuOut);
        }
    }

    public void ChangeScene(string scene)
    {
        FindObjectOfType<AudioManager>().Play("HPblip");
        SceneManager.LoadScene(scene);
    }


    public void SetVolume(float volume)
    {
        FindObjectOfType<AudioManager>().Play("HPblip");
        currentVolume = volume;
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volume", currentVolume);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
