using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Image _livesImg;

    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private Text _restartText;

    private GameManager _gameManager;

    private int _currentPlayerLives;
    //Create a handle to text
    // Start is called before the first frame update
    void Start()
    {
        _currentPlayerLives = 3;
        //_liveSprites[_currentPlayerLives];
        _scoreText.text = "Score: " + 0;
        //Assign text component to the handle
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("GameManager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void updateLives(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];


    }


    public void gameOver()
    {
        _restartText.gameObject.SetActive(true);
        StartCoroutine(displayGameOver());
        _gameManager.GameOver();
    }

    private IEnumerator displayGameOver()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.2f);
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            _gameOverText.gameObject.SetActive(false);
        }
        
    }

    public void setScore(int value)
    {
        _scoreText.text = "Score: " + value;
    }
    
}
