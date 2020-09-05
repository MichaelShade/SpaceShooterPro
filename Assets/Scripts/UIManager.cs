using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour
{
    // handle to text 
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text BestScoreText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    float _flickerRate = 0.5f;
    [SerializeField]
    private Text _restartText;
    private GameManager _gameManager;
    [SerializeField]
    private GameObject PauseMenu;
    private int _currentscore;
    private int _BestScore;
    [SerializeField]
    private Text _ammoText;
    [SerializeField]
    private Slider ThrustSlider;

   // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: 0";
        _ammoText.text = $"Ammo : 15";
        SetBestScoreText();
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("GameManager Is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    // method to update score text
    public void UpdateScoreText(int _score)
    {
        _scoreText.text = "Score: " + _score;
        _currentscore = _score;
    }

    public void CheckForBestScore()
    {
        if (_currentscore > _BestScore)
        {
            _BestScore = _currentscore;
            PlayerPrefs.SetInt("HighScore", _BestScore);
            BestScoreText.text = "Best: " + _BestScore;
        }
    }

    void SetBestScoreText()
    {
        _BestScore = PlayerPrefs.GetInt("HighScore", 0);
        BestScoreText.text = "Best: " + _BestScore;
    }
    

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];
        if (currentLives == 0)
        {

            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
      
        _gameOverText.gameObject.SetActive(true);
        // flicker text 
        StartCoroutine(GameOverFlickerRoutine(_flickerRate));
        // Display restart text
        _restartText.gameObject.SetActive(true);
        _gameManager.GameOver();
    }

    IEnumerator GameOverFlickerRoutine(float FlikerRate)
    {
        while (true)
        {
            yield return new WaitForSeconds(FlikerRate);
            _gameOverText.text = "";
            yield return new WaitForSeconds(FlikerRate);
            _gameOverText.text = "GAME OVER";

        }
    }

    public void TogglePauseMenu()
    {
        PauseMenu.SetActive(!PauseMenu.activeSelf); // finds the current value and sets oposite 
    }


    public void ResumePlay()
    {
        TogglePauseMenu();
        Time.timeScale = 1.0f;
        _gameManager.UnPauseGame();
        
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0); // main menu
    }

    public void UpdateAmmoCountText(int currAmmo)
    {
        _ammoText.text = $"Ammo: {currAmmo}";
    }

    public void UpdateThrustSlider(int value, int maxBoostPower)
    {
        float ThrusterScalingBarHUD =(float) value / (float) maxBoostPower;
        
        ThrustSlider.value = ThrusterScalingBarHUD;
    }
}
