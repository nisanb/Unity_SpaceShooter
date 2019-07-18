using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 100f;

    [SerializeField]
    private int _speedMultiplier = 1;

    /**
     * Powerups
     */
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
 


    [SerializeField]
    private float _fireRate = 0.1f;

    private float _nextFire = 0.0f;

    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;
    private UIManager _uiManager;

    private bool _isTripleShotActive = false;

    [SerializeField]
    private bool _shieldActivated;

    [SerializeField]
    private GameObject ShieldVisualizer;

 

    [SerializeField]
    private int _score;

    void Start()
    {
        _score = 0;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("UIManager is NULL");
        }

        transform.position = new Vector3(0, 0, 0);

        Debug.Log("Spawned player with " + _lives + " life points");

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
        {
            FireLaser();
        }
        
    }

    private void FireLaser()
    {
      
        // New Shot
        _nextFire = Time.time + _fireRate;

        if(_isTripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + (Vector3.up * 1.5f), Quaternion.identity);
        }
        
        
        // if space key press, 
        //  if triple shot active is true
        //   fire 3 lasers (triple shot prefab)
        // else fire 1 laser
        // instantiate triple shot prefab


    }

    private void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * _speedMultiplier * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -8.86f, 8.86f), transform.position.y, 0);

    }
    private void OnTriggerEnter(Collider collider)
    {

    }

    public void damage()
    {
        if (_shieldActivated)
        {
            ShieldVisualizer.SetActive(false);
            _shieldActivated = false;
            return;
        }
        
        _lives--;
        _uiManager.updateLives(_lives);
        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
            _uiManager.gameOver();
        }
    }

    public void activateTripleShot()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerUpRoutine());
    }

    public void activateSpeedBoost()
    {
        _speedMultiplier = 3;
        StartCoroutine(SpeedBoostPowerUpRoutine());
    }
    public void activateShield()
    {
        ShieldVisualizer.SetActive(true);
        _shieldActivated = true;
        StartCoroutine(ShieldPowerUpRoutine());
    }
    IEnumerator SpeedBoostPowerUpRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speedMultiplier = 1;
    }
    IEnumerator TripleShotPowerUpRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }
    IEnumerator ShieldPowerUpRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        ShieldVisualizer.SetActive(false);
        _shieldActivated = false;
    }

    public void addKill(int points)
    {
        _score += points;
        _uiManager.setScore(_score);
    }

    //method to add 10 to the score!
    //Communicate with the UI to update the score
}
