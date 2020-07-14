using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver;
    private bool isGamwPaused;
    [SerializeField]
    private bool isCoOp = false;
    [SerializeField]
    private UIManager _UiManager;
    [SerializeField]
    private Animator _PauseMenuAnimator;


    private void Start()
    {
        if (_PauseMenuAnimator == null)
            Debug.LogError("Please assign Pause menu animator ");
        else
        {
            _PauseMenuAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }
       
    }

    private void Update()
    {
        // if the r key is pressed and game is over 
        //load current scene 
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver && !isCoOp)
        {
            SceneManager.LoadScene(1); // single player
        }

        if (Input.GetKeyDown(KeyCode.R) && _isGameOver && isCoOp)
        {
            SceneManager.LoadScene(2); // coOp scene
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Application.Quit();
            SceneManager.LoadScene(0); // main menu
        }

        if (Input.GetKeyDown(KeyCode.P ) && !isGamwPaused)
        {
            if(_UiManager == null)
            {
                Debug.LogError("Please assign UI Manager");
            }
            else
            {
                isGamwPaused = true;

                _UiManager.TogglePauseMenu();
                _PauseMenuAnimator.SetBool("IsPaused", true);
                //display pause screen
               

                // pause game
                Time.timeScale = 0;
            }

        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
    public void UnPauseGame()
    {
        isGamwPaused = false;
    }
}
