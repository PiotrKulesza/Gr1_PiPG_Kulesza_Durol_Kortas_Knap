using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipDisplayer : MonoBehaviour
{
    private bool showTip = false;
    private float timer = 0;
    public float tipTime = 5;
    public GUIText tipGUI;

    // Update is called once per frame
    void Update()
    {
        if (showTip)
        {
            if (timer < tipTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                tipGUI.enabled = false;
                showTip = false;
                timer = 0;
            }
        }
    }

    void displayTipMessage(string tipText)
    {
        tipGUI.text = tipText;
        tipGUI.enabled = true;
        this.showTip = true;
    }
}
