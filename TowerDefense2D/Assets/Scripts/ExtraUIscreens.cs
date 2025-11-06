using UnityEngine;
using UnityEngine.SceneManagement;

public class ExtraUIscreens : MonoBehaviour
{
    public void OnQuit()
    {
        Application.Quit();
    }
    public void OnStart()
    {
        SceneManager.LoadScene(1);
    }
}
