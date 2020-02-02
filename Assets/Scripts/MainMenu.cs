using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject creditsPanel;
    public GameObject menuPanel;

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void ChangeScene(int id)
    {
        SceneManager.LoadScene(id);
    }

}
