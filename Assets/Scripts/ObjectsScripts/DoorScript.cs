using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private string sceneNameToLoad;

    void Awake()
    {
        GlobalEventManager.onLocationDoorClicked.AddListener(LoadScene);
        string name = gameObject.name;
        //Debug.Log("Я подписался! " + name);
    }

    public void LoadScene(string doorName)
    {
        if (gameObject.name == doorName)
        {
            if (!string.IsNullOrEmpty(sceneNameToLoad))
            {
                SceneManager.LoadScene(sceneNameToLoad);
                //Debug.Log("Я загрузил " + sceneNameToLoad);
            }
        }
    }
}
