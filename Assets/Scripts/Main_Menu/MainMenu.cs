using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public void LoadGame()
    {
        SceneManager.LoadScene(1); // main game scene 
    }

    public void LoadCoOpGame()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
