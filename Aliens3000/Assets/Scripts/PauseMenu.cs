using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [HideInInspector]
    public string mainMenuSceneName = "Menu";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            LoadMainMenu();
        }
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}