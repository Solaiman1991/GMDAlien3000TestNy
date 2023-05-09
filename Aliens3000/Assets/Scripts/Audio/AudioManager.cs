
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public int maxConcurrentInstances = 3;
    public Dictionary<string, int> soundInstances = new Dictionary<string, int>();

    private void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            
            sound.source.clip = sound.audioClip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.rolloffMode = AudioRolloffMode.Logarithmic;
            sound.source.minDistance = sound.minDistance;
            sound.source.maxDistance = sound.maxDistance;
        }
    }

    public void PlaySound(string soundName, bool loop = false)
    {
        Sound s = Array.Find(sounds, sound => sound.audioName == soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + soundName + " not found!");
            return;
        }

        s.source.loop = loop;
        s.source.Play();
    }
   
    public void StopSound(string soundName)
    {
        Sound sound = Array.Find(sounds, s => s.audioName == soundName);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + soundName + " not found!");
            return;
        }
    
        sound.source.Stop();
    }
    
    public bool IsSoundPlaying(string soundName)
    {
        Sound sound = Array.Find(sounds, s => s.audioName == soundName);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + soundName + " not found!");
            return false;
        }

        return sound.source.isPlaying;
    }
    
}
