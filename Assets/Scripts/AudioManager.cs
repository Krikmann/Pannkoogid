using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Dictionary<string, Sound> soundsDict = new Dictionary<string, Sound>();
    public float masterVolume = 1f;
    Sound gameMusic;
    Sound gameAmbient;

    void Awake()
    {
        GlobalReferences.audioManager = gameObject.GetComponent<AudioManager>();
        foreach (Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.playOnAwake = false;
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            soundsDict.Add(sound.name, sound);
        }
        Debug.Log("Size: " + soundsDict.Count);
    }

    void Update()
    {
        PlayMusic();
        playAmbient();
    }
    public void PlayMusic()
    {
        
    }
    
    public void playAmbient()
    {

        if (SceneManager.GetActiveScene().buildIndex >= 2 || SceneManager.GetActiveScene().buildIndex <=6)
        {
            gameAmbient = soundsDict["AMBIENT"];
            if (!gameAmbient.audioSource.isPlaying)
            {
                gameAmbient.audioSource.loop = true;
                playSound(gameAmbient.name);
            }
        }
    }

    public void playERM()
    {
        
        int num  = UnityEngine.Random.Range(0, 3);
        playSound("ERM_"+num.ToString(), 1, 1);
    }
    public void playFakeERM()
    {   
        int num  = UnityEngine.Random.Range(0, 3);
        playSound("FAKE_ERM_"+num.ToString(), 1, 1);
    }


    public void playSound(string name)
    {
        playSound(name, 1, 1);
    }

    public void playSound(string name, float volumeChange, float pitchChange)
    {
        Sound sound = soundsDict[name];
        sound.audioSource.volume = sound.volume * volumeChange * GlobalReferences.volume;
        sound.audioSource.pitch = sound.pitch * pitchChange;
        sound.audioSource.Play();
    }

    
}
