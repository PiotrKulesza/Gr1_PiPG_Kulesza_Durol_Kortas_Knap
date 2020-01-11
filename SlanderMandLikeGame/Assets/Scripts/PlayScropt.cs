using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScropt : MonoBehaviour
{

    public Color OnMouseOverColor = Color.red;
    public Color OnMouseExitColor = Color.white;

    public static int paper = 0;

    void collectPaper()
    {
        paper++;
    }


    void OnMouseOver()
    {
        GetComponent<TextMesh>().color = OnMouseOverColor;
    }
    void OnMouseExit()
    {
        GetComponent<TextMesh>().color = OnMouseExitColor;
    }

    void OnMouseUp()
    {
        SceneManager.LoadScene(1);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



    }
}
