using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paper : MonoBehaviour
{



    public GameObject obiektDoPodniesienia;
    // Start is called before the first frame update
    void Start()
    {
        
    }

  
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Przedmiot")
        {
            Debug.Log("You`ve found one paper!");
        }
    }
}
