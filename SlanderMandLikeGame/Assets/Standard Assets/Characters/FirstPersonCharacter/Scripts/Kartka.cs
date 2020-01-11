using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kartka : MonoBehaviour
{
 public GameObject podnies;
    bool czyMoge;
   
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(czyMoge == true && podnies != null)
        {
            if (Input.GetKeyDown (KeyCode.E))
            {
                Destroy(podnies);
                podnies = null;
                MonsterMove.MoveSpeed += 1f;
                Collection.coins += 1;
       
            }
            
        } 
       

    }
    
     void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Pappier")
        {
            podnies = col.gameObject;
            czyMoge = true;
           //col.SendMessage("Collection");
        }

    }
    
}
