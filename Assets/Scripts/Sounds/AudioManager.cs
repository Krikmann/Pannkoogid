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
    }

    void Update()
    {
        PlayMusic();
        playAmbient();
    }
    public void PlayMusic()
    {
        if (SceneManager.GetActiveScene().buildIndex <= 1 || SceneManager.GetActiveScene().buildIndex == 7)
        {
            gameMusic = soundsDict["MUSIC"];
            gameMusic.audioSource.volume = gameMusic.volume*GlobalReferences.volume;
            if (!gameMusic.audioSource.isPlaying)
            {
                gameMusic.audioSource.loop = true;
                playSound(gameMusic.name);
            }
        }
    }
    
    public void playAmbient()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 2 || SceneManager.GetActiveScene().buildIndex <=6)
        {
            gameAmbient = soundsDict["AMBIENT"];
            gameAmbient.audioSource.volume = gameAmbient.volume*GlobalReferences.volume;
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
    public void playCORRECT()
    {
        playSound("CORRECT", 1, 1);
    }
    public void playINCORRECT()
    {
        playSound("INCORRECT", 1, 1);
    }
    public void playWHOOSH()
    {
        playSound("WHOOSH", 1, 1);
    }
    public void playFAHHH()
    {
        playSound("FAHHH", 1, 1);
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
