using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private bool _canClick = true;
    public bool fadeIn = true;
    [SerializeField] private Image _backDrop;

    void Start()
    {
        if (fadeIn)
        {
            StartCoroutine(FadeIn());
        }
        if (SceneManager.GetActiveScene().name == "Title")
        {
            AudioManager.Instance.PlayMusic(AudioManager.Music.Menu);
        }
        if (SceneManager.GetActiveScene().name == "Main")
        {
            AudioManager.Instance.PlayMusic(AudioManager.Music.Level);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnPlay()
    {
        if (_canClick)
        {
            _canClick = false;
            StartCoroutine(FadeOutAndLoadScene("Tutorial"));
        }
    }

    public void OnCredits()
    {
        if (_canClick)
        {
            _canClick = false;
            StartCoroutine(FadeOutAndLoadScene("Credits"));
        }
    }

    public void OnGoToGame()
    {
        if (_canClick)
        {
            _canClick = false;
            StartCoroutine(FadeOutAndLoadScene("Main"));
        }
    }

    public void OnRetry()
    {
        if (_canClick)
        {
            _canClick = false;
            StartCoroutine(FadeOutAndLoadScene("Main"));
        }
    }

    public void OnBackToTitle()
    {
        if (_canClick)
        {
            _canClick = false;
            StartCoroutine(FadeOutAndLoadScene("Title"));
        }
    }

    public void OnSwitchSceneTo(string name)
    {
        if (_canClick)
        {
            _canClick = false;
            StartCoroutine(FadeOutAndLoadScene(name));
        }
    }


    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        float time = 0;
        while (time < 0.5f)
        {
            time += Time.deltaTime;
            if (time > 0.5f)
                time = 0.5f;
            _backDrop.color = new Color(46 / 255.0f, 44 / 255.0f, 59 / 255.0f, time * 2);
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeIn()
    {
        _canClick = false;
        float time = 0.5f;
        while (time > 0)
        {
            time -= Time.deltaTime;
            if (time < 0)
                time = 0;
            _backDrop.color = new Color(46 / 255.0f, 44 / 255.0f, 59 / 255.0f, time * 2);
            yield return null;
        }
        _canClick = true;
    }
}
