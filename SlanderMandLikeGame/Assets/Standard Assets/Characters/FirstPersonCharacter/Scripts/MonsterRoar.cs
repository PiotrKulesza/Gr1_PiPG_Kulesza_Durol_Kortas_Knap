using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRoar : MonoBehaviour
{
    public GameObject Player;
    public AudioClip Roar;
    public AudioSource MusicSource;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MusicSource.clip = Roar;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Player.tag)
        {
            MusicSource.Play();

        }
    }
}