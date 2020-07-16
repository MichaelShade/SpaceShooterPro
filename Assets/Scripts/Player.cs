using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Scene currentScene;
    public float speed = 3.5f;
    [SerializeField]
    public float _ThrusterBoost = 2f;
    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;

    // Start is called before the first frame update

    Animator anim;

    [SerializeField]
    private int _MaxAmmo = 15;
    private int _CurrAmmo;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _trippleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private GameObject _ShieldVisualizer;
    [SerializeField]
    private int NumShieldHits = 3;
    private int CurrentSheildStr = 3;
    [SerializeField]
    private Color SheieldDamagedColor1;
    [SerializeField]
    private Color SheieldDamagedColor2;
    private SpriteRenderer SheieldRend;

    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private GameObject _rightEngine;

    [SerializeField]
    private GameObject _leftEngine;

    // Var for audio clip
    private AudioSource _aSource;
    [SerializeField]
    private AudioClip LazerAudio;

    SpawnManager _spawnManager;

    UIManager _uiManager;

    // var for tripple shot active
    private bool _isTrippleShotActive = false;
    [SerializeField]
    private float _trippleShotCoolDown = 5f;

    // var for speed boost 
    //8.5 
    [SerializeField]
    private float _BoostSpeed = 8.5f;
    private bool _IsSpeedBoostActive = false;
    [SerializeField]
    private float _SpeedBoostCoolDown = 5f;

    //Var for Shield
    [SerializeField]
    bool _IsShieldActive = false;
    [SerializeField]
    private int _score = 0;


    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        if (!SceneIsCoOp(currentScene))
        {
            transform.position = new Vector3(0, 0, 0);
            _CurrAmmo = 15;

        }
        else
        {
            // Set CoOp stuff
        }
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is null");
        }
        if (_uiManager == null)
        {
            Debug.Log("COuld not find Ui manager");
        }

        if (_ShieldVisualizer == null)
        {
            Debug.Log("Please assign the shield Visualizer");
        }
        else
        {
            _ShieldVisualizer.SetActive(false);
            SheieldRend = _ShieldVisualizer.GetComponent<SpriteRenderer>();
        }

        if (_leftEngine == null || _rightEngine == null)
        {
            Debug.LogError("Please assign both ENgine objects in the inspector");
        }
        else
        {
            _leftEngine.SetActive(false);
            _rightEngine.SetActive(false);
        }

        _aSource = GetComponent<AudioSource>();
        if (_aSource == null)
        {
            Debug.LogError("Please add Audio SOurce Componant");
        }

        anim = GetComponent<Animator>();

    }

    private bool SceneIsCoOp(Scene currentScene)
    {
        if (currentScene.buildIndex == 2)
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {

        if (this.isPlayerOne)
        {
            CalculateMovement1();
        }
        else if (this.isPlayerTwo)
        {
            CalculateMovement2();
        }


#if UNITY_ANDROID
        if ((Input.GetKeyDown(KeyCode.Space) && this.isPlayerOne) || (CrossPlatformInputManager.GetButtonDown("Fire") && Time.time > _canFire && this.isPlayerOne))
        {
            FireLazer();
        }

#else
        if ((Input.GetKeyDown(KeyCode.Space) && this.isPlayerOne) || (Input.GetMouseButtonDown(0) && Time.time > _canFire && this.isPlayerOne && _CurrAmmo > 0))
        {
            FireLazer();
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter) && (Time.time > _canFire && this.isPlayerTwo))
        {
            FireLazer();
        }


#endif



        if (Input.GetKeyDown(KeyCode.Space) && this.isPlayerOne || Input.GetMouseButtonDown(0) && Time.time > _canFire && this.isPlayerOne && _CurrAmmo > 0)
        {
            FireLazer();
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter) && (Time.time > _canFire && this.isPlayerTwo))
        {
            FireLazer();
        }



    }

    #region Movement
    private void CalculateMovement1()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal"); //Input.GetAxis("Horizontal");
        float verticalInput = CrossPlatformInputManager.GetAxis("Vertical"); // Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        CalculateBoost(direction);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0f), 0f);
        WrapXMovement();

        anim.SetFloat("Strafe", horizontalInput);




    }

    private void CalculateMovement2()
    {
        float horizontalInput = Input.GetAxis("Horizontal2");
        float verticalInput = Input.GetAxis("Vertical2");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        CalculateBoost(direction);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0f), 0f);
        WrapXMovement();
        anim.SetFloat("Strafe", horizontalInput);

    }

    private void CalculateBoost(Vector3 _dir)
    {
        // Check to see if should be thrusting

        if (Input.GetKey(KeyCode.LeftShift) && !_IsSpeedBoostActive)
        {
            transform.Translate(_dir * (_ThrusterBoost + speed) * Time.deltaTime);

        }


        if (_IsSpeedBoostActive)
        {
            transform.Translate(_dir * _BoostSpeed * Time.deltaTime);

        }
        else
        {
            transform.Translate(_dir * speed * Time.deltaTime);
        }




    }

    private void WrapXMovement()
    {
        if (transform.position.x >= 11.2)
        {
            transform.position = new Vector3(-11.4f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -11.4)
        {
            transform.position = new Vector3(11.2f, transform.position.y, transform.position.z);
        }
    }


    #endregion

    void FireLazer()
    {

        _canFire = Time.time + _fireRate;

        if (_isTrippleShotActive)
        {
            Instantiate(_trippleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Vector3 offset = new Vector3(0, 1.05f, 0);
            Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity);

        }

        // play the lazer Audio clip
        if (LazerAudio != null)
        {
            _aSource.PlayOneShot(LazerAudio);
            _CurrAmmo--;
            _uiManager.UpdateAmmoCountText(_CurrAmmo);
        }
        else
        {
            Debug.LogWarning("Please assign Lazer Audio Clip");
        }


    }

    public void Damage()
    {



        if (_IsShieldActive)
        {
            if (CurrentSheildStr > 1)
            {
                ShieldCtl();

                return;
            }

            else
            {
                ResetShields();

                return;
            }


        }
        _lives--;
        // if lives is 2 enable right engine
        if (_lives == 2)
        {
            _rightEngine.SetActive(true);
        }
        else if (_lives == 1)
        {
            _leftEngine.SetActive(true);
        }


        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            if (_explosionPrefab != null)
            {
                Instantiate(_explosionPrefab, gameObject.transform.position, Quaternion.identity);
            }
            _spawnManager.OnPlayerDeath();
            _uiManager.CheckForBestScore();


            Destroy(this.gameObject);
        }

    }

    private void ResetShields()
    {
        _IsShieldActive = false;
        if (_ShieldVisualizer != null)
        {

            _ShieldVisualizer.SetActive(false);
            SheieldRend.color = Color.white;

        }

        CurrentSheildStr = NumShieldHits;
    }

    private void ShieldCtl()
    {
        switch (CurrentSheildStr)
        {
            case 3:
                CurrentSheildStr--;
                SheieldRend.color = SheieldDamagedColor1;
                break;

            case 2:
                CurrentSheildStr--;
                SheieldRend.color = SheieldDamagedColor2;

                break;

            default:
                break;
        }
    }

    public void TrippleShotActive()
    {
        _isTrippleShotActive = true;
        StartCoroutine(TrippleShotPowerdownRoutine());
    }

    public void SpeedBostActive()
    {
        _IsSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public void ShieldActive()
    {
        _IsShieldActive = true;
        if (_ShieldVisualizer != null)
            _ShieldVisualizer.SetActive(true);
    }

    public void Score(int _value)
    {
        _score += _value;
        if (_uiManager != null)
        {
            _uiManager.UpdateScoreText(_score);
        }
        // update UI
    }

    IEnumerator TrippleShotPowerdownRoutine()
    {
        yield return new WaitForSeconds(_trippleShotCoolDown);
        _isTrippleShotActive = false;

    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(_SpeedBoostCoolDown);
        _IsSpeedBoostActive = false;
    }




}
