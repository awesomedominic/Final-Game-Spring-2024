using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private CrossFade _crossFade;
    [SerializeField] private Endpoint _endpoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_endpoint.HasReached())
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