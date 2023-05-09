using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour
{
    
    public float FrontWave = 0.5f;
    public float SideWave = 0.5f;
    float scatter;

    void Awake()
    {
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks * 1000);
    }

    void Start()
    {
        scatter = Random.Range(-0.5f, 0.5f);
    }

    void Update()
    {
        Vector3 initialPosition = transform.localPosition;
        initialPosition.y += (FrontWave * Mathf.Sin(Time.time * (2.8f + scatter))) / 200f;
        initialPosition.x += (SideWave * Mathf.Sin(Time.time * (3.4f + scatter))) / 300f;
        transform.localPosition = initialPosition;
    }
    
 
}

    

