using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] GameObject turnOnMusic;
    [SerializeField] GameObject turnOffMusic;
    [SerializeField] GameObject turnOnSound;
    [SerializeField] GameObject turnOffSound;

    public AudioClip background;
    public AudioClip typing;
    public AudioClip image_change;   

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    } 

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void StopSFX()
    {
        SFXSource.Stop();
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

    public void StopSound()
    {
        turnOnSound.SetActive(true);
        turnOffSound.SetActive(false);

        SFXSource.volume = 0;
    }
    
    public void PlaySound()
    {
        turnOnSound.SetActive(false);
        turnOffSound.SetActive(true);

        SFXSource.volume = 1;
    }
}
