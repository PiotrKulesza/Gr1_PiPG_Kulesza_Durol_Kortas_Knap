using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class koniec : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "katka")
        {
            if (Dodawanie.coins == 3)
            {
                Application.Quit();
            }
        }
    }

}
