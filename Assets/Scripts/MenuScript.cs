using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject instructions;
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject credits;
    public GameObject pauseMenuContainer;
    public GameObject notifyPlayer;

    public bool isPauseMenu;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }


    public void Update()
    {
        PauseMenu();
    }


    public void InstructionsButton()
    {
        instructions.SetActive(true);
        buttons.SetActive(false);
    }

    public void CreditsButton()
    {
        credits.SetActive(true);
        buttons.SetActive(false);
    }

    public void BackCreditsButton()
    {
        credits.SetActive(false);
        buttons.SetActive(true);
    }

    public void BackButton()
    {
        instructions.SetActive(false);
        buttons.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuContainer.SetActive(true);
            Time.timeScale = 0f;
            notifyPlayer.gameObject.GetComponent<PlayerInMenu>().inMenu = true;
        }
    }

    public void OnResume()
    {
        pauseMenuContainer.SetActive(false);
        Time.timeScale = 1f;
        notifyPlayer.gameObject.GetComponent<PlayerInMenu>().inMenu = false;
    }
}