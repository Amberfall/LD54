using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static UnityEvent deathEvent = new UnityEvent();
    public bool isPlayerAlive = true;

    [SerializeField] private DeathScreen _deathScreen;
    [SerializeField] private Transform _cameraTarget;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerDied(Vector3 position)
    {
        isPlayerAlive = false;
        StartCoroutine(PlayerDiedCoroutine(position));
    }

    private IEnumerator PlayerDiedCoroutine(Vector3 position)
    {
        AudioManager.Instance.StopMusic();
        _deathScreen.gameObject.SetActive(true);
        _deathScreen.transform.position = position;
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime;
            if (time > 1)
                time = 1;
            _deathScreen.SetAlpha(time);
            yield return null;
        }
        time = 0;
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.PlayerKilled);
        while (time < 1)
        {
            time += Time.deltaTime;
            if (time > 1)
                time = 1;
            _cameraTarget.position = Vector3.Lerp(_deathScreen.transform.position, Vector3.zero, time);
            _deathScreen.SetScale(3 * time + 1);
            yield return null;
        }
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.GameOver);
        // TODO: Play player death animation
    }
}
