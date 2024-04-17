using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject instructions;
    [SerializeField] private GameObject buttons;

    public void StartGame()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void InstructionsButton()
    {
        instructions.SetActive(true);
        buttons.SetActive(false);
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
}