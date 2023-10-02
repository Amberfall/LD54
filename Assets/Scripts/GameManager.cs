using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static UnityEvent deathEvent = new UnityEvent();
    public bool isPlayerAlive = true;

    [SerializeField] private DeathScreen _deathScreen;
    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private CinemachineConfiner2D _cinemachineConfiner2D;
    public bool checkForWinCondition = false;
    private void Awake()
    {
        instance = this;
        Cursor.visible = false;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (checkForWinCondition)
        {
            checkForWinCondition = false;
            StartCoroutine(CheckForWinCoroutine());
        }
    }

    IEnumerator CheckForWinCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            {
                int n = FindObjectsOfType<Enemy>().Length;
                if (n == 0)
                {
                    SceneManager.LoadScene("Win");
                }
            }
        }
    }

    public void PlayerDied(Vector3 position)
    {
        isPlayerAlive = false;
        StartCoroutine(PlayerDiedCoroutine(position));
    }

    private IEnumerator PlayerDiedCoroutine(Vector3 position)
    {
        AudioManager.Instance.StopVacuum();
        AudioManager.Instance.StopMusic();
        _deathScreen.gameObject.SetActive(true);
        _deathScreen.transform.position = position;
        _cameraTarget.position = _cinemachineConfiner2D.transform.position;
        var ct = _cameraTarget.GetComponent<CameraTarget>();
        //_cinemachineConfiner2D.enabled = false;
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime;
            if (time > 1)
                time = 1;
            ct.cameraOffset = Vector3.Lerp(ct.cameraOffset, Vector3.zero, time);
            _deathScreen.SetAlpha(time);
            yield return null;
        }
        time = 0;
        AudioManager.Instance.PlayGameOverSfx();
        while (time < 1)
        {
            time += Time.deltaTime;
            if (time > 1)
                time = 1;
            //_cameraTarget.position = Vector3.Lerp(_cameraTarget.position, _deathScreen.transform.position, time);
            _deathScreen.SetScale(3 * time + 1);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameOver");

        // TODO: Play player death animation
    }
}
