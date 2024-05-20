using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    [SerializeField] private int _currentLevelNumber = 1;
    [SerializeField] private bool _levelOneCompleted, _levelTwoCompleted, _levelThreeCompleted, _gameIsCompleted;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        
    }
    
    public int GetCurrentLevelNumber()
    {
        return _currentLevelNumber;
    }

    public void CompletedLevel(string levelName)
    {
        switch (levelName)
        {
            case "Level 1": _levelOneCompleted = true;
            break;
            case "Level 2": _levelTwoCompleted = true;
            break;
            case "Level 3": _levelThreeCompleted = true; 
            break;
            default: _gameIsCompleted = true;
            break;
        }

        if(_levelOneCompleted)
        {
            _currentLevelNumber = 2;
        }
        if(_levelTwoCompleted)
        {
            _currentLevelNumber = 3;
        }
        if(_levelThreeCompleted)
        {
             ResetGame();
        }
    }

    public void ResetGame()
    {
        _currentLevelNumber = 1;
        _levelOneCompleted = false;
        _levelTwoCompleted = false;
        _levelThreeCompleted = false;
        _gameIsCompleted = false;
    }
}
