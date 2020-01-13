using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class MonsterMove : MonoBehaviour
{

    public Transform Player;
    public static float MoveSpeed = 2f;
    public int DeadDist = 3;
    public int RawrDist = 10;
    


    // Start is called before the first frame update
    void Start()
    {
        MoveSpeed = 2f;
        transform.position = new Vector3(Player.position.x - 50, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player);
        if (Vector3.Distance(transform.position, Player.position) > DeadDist)
        {
            
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
          

           if (transform.position.y > 0)
           {
                transform.position =  new Vector3(this.transform.position.x,0.0f,this.transform.position.z);
           }


            if (Vector3.Distance(transform.position, Player.position) <= RawrDist)
            {

            }
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }
}


