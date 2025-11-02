using UnityEngine;
using UnityEngine.SceneManagement;

public class NewDoorScript : MonoBehaviour
{
    [SerializeField] private string sceneNameToLoad;

    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneNameToLoad))
        {
            SceneManager.LoadScene(sceneNameToLoad);
        }
    }
}
