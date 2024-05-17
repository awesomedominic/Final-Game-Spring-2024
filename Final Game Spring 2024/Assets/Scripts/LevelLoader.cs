using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private CrossFade _crossFade;
    [SerializeField] private Endpoint _endpoint;
    [SerializeField] private Timer _timer;

    // Start is called before the first frame update
    void Start()
    {
        _crossFade.FadeOut();
        _timer.StartGameTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if(_endpoint.HasReached() || !_timer.GetGameTimer())
        {
            StartCoroutine("EndLevel");
        }
    }

    IEnumerator EndLevel()
    {
        _crossFade.FadeIn();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Level Select");
    }
}