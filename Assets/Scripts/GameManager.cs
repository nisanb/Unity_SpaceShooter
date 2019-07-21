using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            restartScene();
        }
    }

    private void restartScene()
    {
        SceneManager.LoadScene(1); // Current game scene
    }
    public void GameOver()
    {
        _isGameOver = true;
    }



}
