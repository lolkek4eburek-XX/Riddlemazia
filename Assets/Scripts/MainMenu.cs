using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int firstLevel = 1;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(firstLevel);
    }
}
