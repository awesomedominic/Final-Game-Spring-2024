using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private CrossFade _crossFade;
    [SerializeField] private Endpoint _endpoint;
    [SerializeField] private Timer _timer;
    [SerializeField] private string _sceneName;

    // Start is called before the first frame update
    void Start()
    {
        _crossFade.FadeOut();
        _timer.StartGameTimer();
        _sceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if(_endpoint.HasReached() || !_timer.GetGameTimer())
        {
            GameManager.Instance.CompletedLevel(_sceneName);
            StartCoroutine("EndLevel");
        }

        if(_endpoint.HasReached() && _sceneName == "Level 3")
        {
            StartCoroutine("YouWonGame");
        }
    }

    IEnumerator EndLevel()
    {
        _crossFade.FadeIn();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Level Select");
    }

    IEnumerator YouWonGame()
    {
        _crossFade.FadeIn();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("You Won");
    }
}