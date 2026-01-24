using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Dictionary<string, Sound> soundsDict = new Dictionary<string, Sound>();
    public float masterVolume = 1f;
    Sound gameMusic;

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

    public void PlayMusic()
    {
        
    }

    public void playERM()
    {
        playSound("ERM_LOW_0", 1, 1);
    }
    public void playFakeERM()
    {
        playSound("FAKE_ERM_LOW_0", 1, 1);
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
