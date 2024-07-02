using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundFXObject;

    [Header("Background Music")]
    [SerializeField] private AudioClip backgroundMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume, string sfxObjectTag)
    {
        //SPAWN IN GAME OBJECT
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        //ASSIGN AUDIO CLIP
        audioSource.clip = audioClip;

        //ASSIGN VOLUME
        audioSource.volume = volume;

        //ASSIGN TAG FOR THE SFX OBJ
        audioSource.gameObject.tag = sfxObjectTag;


        //PLAY SOUND
        audioSource.Play();

        //GET LENGTH OF SOUND FX CLIP
        float clipLength = audioSource.clip.length;

        //DESTROY THE CLIP AFTER IT IS DONE PLAYING
        Destroy(audioSource.gameObject, clipLength);
    }


    public void StopBackgroundMusic()
    {
        musicSource.Stop();
    }

    public void PauseBackgroundMusic()
    {
        musicSource.Pause();
    }

    public void ResumeBackgroundMusic()
    {
        musicSource.UnPause();
    }
}
