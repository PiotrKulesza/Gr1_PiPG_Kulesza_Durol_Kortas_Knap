using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kartka : MonoBehaviour
{
 public GameObject podnies;
    bool czyMoge;
    // DÓŁ
    public static int coins = 0;
    void collectCoin()
    {
        coins--;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(czyMoge == true)
        {
            if (Input.GetKeyDown (KeyCode.E))
            {
                Destroy(podnies);
                podnies = null;
                
            }
            
        } 
       

    }
    //     void OnTriggerEnter(Collider other)
    //     {
    //         if (other.tag == "Pappier")
    //         {
    //            Debug.Log("You`ve found one paper!");
    //            Destroy(gameObject);
    //        }
    //    }
     void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Pappier")
        {
            podnies = col.gameObject;
            czyMoge = true;
         //   col.SendMessage("Collection");
        }
        if(Collection.coins == 3)
        {
            Application.Quit();
        }
    }
 //   void OnTriggerExit(Collider col)
 //   {
  //   if(col.tag == "Pappier")
  //      {
  //          podnies = null;
  //          czyMoge = false;
            
  //      }   
//    }

    
}
