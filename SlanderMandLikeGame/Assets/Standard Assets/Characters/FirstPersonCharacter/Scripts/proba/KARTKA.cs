using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KARTKA : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
      
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "katka")
        {
          //  col.SendMessage("CollectCoin");
             Destroy(gameObject);
            Application.Quit();
        }
    }

}
