using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject HowtoPanel;
    public GameObject MenuPanel;
   
   public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void openPanel()
    {
        HowtoPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }

    public void closePanel()
    {
        HowtoPanel.SetActive(false);
    }
}
