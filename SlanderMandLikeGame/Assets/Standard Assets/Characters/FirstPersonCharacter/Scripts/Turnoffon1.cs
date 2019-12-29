using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turnoffon1 : MonoBehaviour
{
    public bool on = false;
    public AudioClip MusicClip;
    public AudioSource MusicSource;
    void Start()
    {
        MusicSource.clip = MusicClip;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            on = !on;
            MusicSource.Play();
        }
            if (on)
            GetComponent<Light>().enabled = true;
        else if (!on)
            GetComponent<Light>().enabled = false;
    }
}
