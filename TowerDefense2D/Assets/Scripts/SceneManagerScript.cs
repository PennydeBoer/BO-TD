
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("DragAndDrop",LoadSceneMode.Additive);
    }
}
