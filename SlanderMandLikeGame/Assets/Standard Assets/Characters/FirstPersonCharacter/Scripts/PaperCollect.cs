using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperCollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Przedmiot")
        {
            Debug.Log("You`ve found one paper!");
        }
    }
}
