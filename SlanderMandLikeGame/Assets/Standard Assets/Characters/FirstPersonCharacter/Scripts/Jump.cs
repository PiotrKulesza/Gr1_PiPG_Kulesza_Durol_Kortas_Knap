using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Jump : MonoBehaviour
{
    // Your audio clip
    public AudioClip MusicClip;

    // the component that Unity uses to play your clip
    public AudioSource MusicSource;
    float timer = 0.0f;
    public float seconds;
    // Use this for initialization
    void Start()
    {
        MusicSource.clip = MusicClip;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        seconds = (float)(timer % 60);
        print(seconds);
        if (Input.GetKeyDown(KeyCode.Space) && seconds >0.9)
        {
            
            MusicSource.Play();
            timer = 0;
           
        }
            

    }

   
}

