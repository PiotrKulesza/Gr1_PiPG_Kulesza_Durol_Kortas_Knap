using UnityEngine.SceneManagement;

public static class Loader 
{

    public enum Scene
    {
        SampleScene,
    }

   
    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }



}
