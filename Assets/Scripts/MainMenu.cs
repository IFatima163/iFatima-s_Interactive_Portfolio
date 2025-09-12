using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] GameObject turnOnMusic;
    [SerializeField] GameObject turnOffMusic; 

    public AudioClip background;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }    

    public void StopMusic()
    {
        turnOnMusic.SetActive(true);
        turnOffMusic.SetActive(false);

        musicSource.volume = 0;
    }
    
    public void PlayMusic()
    {
        turnOnMusic.SetActive(false);
        turnOffMusic.SetActive(true);

        musicSource.volume = 1;
    }
}
