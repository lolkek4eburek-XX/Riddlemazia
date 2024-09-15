using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    public AudioSource music;
    public static GameObject player;
    public bool isOpened = false;
    public GameObject[] ui;
    public GameObject menu;
    bool joystickControl = false;
    public int mainMenu = 0;
    public Slider slider;

    public void Start()
    {
        Time.timeScale = 1;
        isOpened = false;
        music = music.GetComponent<AudioSource>();
        menu.SetActive(isOpened);
        joystickControl = GetComponent<PlayerController>().joystickControl;
    }
    public void Open()
    {
        music.Pause();
        Time.timeScale = 0;
        menu.SetActive(true);
        for (int i = 0; i < ui.Length; i++)
        {
            ui[i].SetActive(false);
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
    public void Close()
    {
        music.Play();
        menu.SetActive(false);
        Time.timeScale = 1;
        if (!joystickControl)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        for (int i = 0;i < ui.Length; i++)
        {
            ui[i].SetActive(true);
        }
    }
    public void SwitchMenu()
    {
        if (isOpened)
        {
            Close();
        }
        else
        {
            Open();
        }
    isOpened = !isOpened;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchMenu();
        }
    }

    public void SetMusicVolume()
    {
        music.volume = slider.value;
    }

    public void Main()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
