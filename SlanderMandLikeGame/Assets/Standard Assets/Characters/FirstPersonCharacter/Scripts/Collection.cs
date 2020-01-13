using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collection : MonoBehaviour
{
    public static int coins = 0;


    // Start is called before the first frame update
    void Start()
    {
        coins = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (coins > 2)
        
        

        if (coins == 5)
        SceneManager.LoadScene(3);
        
    }
}

    


    
