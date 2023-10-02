using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private bool _canClick;
    [SerializeField] private Image _backDrop;

    void Start()
    {
        StartCoroutine(FadeIn());
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
        SceneManager.LoadScene("Tutorial");
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
