using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void TestScene()
    {
        SceneManager.LoadScene(1);    
    }
    public void StartGame ()
    {
        SceneManager.LoadScene(2);
    }
    public void MainMenuScene ()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Debug.Log("Игра закрылась");
        Application.Quit();
    }
}
