using System.Security.Cryptography.X509Certificates;
using UnityEngine.Audio;
using UnityEngine;
[System.Serializable]

public class Sound


{
    public string audioName;
    public AudioClip audioClip;

    [Range(0f,1f)]
    public float volume;
    [Range(1f,3f)]
    public float pitch; 
    public bool loop;
    
    [Range(0f, 1f)]
    public float spatialBlend;
    
    public float minDistance = 1f; 
    public float maxDistance = 500f; 
    
    [HideInInspector]
    public AudioSource source;



}
