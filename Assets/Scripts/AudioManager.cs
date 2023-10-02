using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public enum Music { Menu, Level };
    public enum Sfx { PlayerDamaged, PlayerKilled, VacuumNoise, PortalSpawn, DemonDamaged, PowerUp, PlayerShoot, ItemAlert, VacuumSuck, Footsteps, GameOver, DogBark, EnemyDamaged, Thud, Dash, UpgradeEaten, EnemyShoot, Crash, OverHere, HealthUp };

    [SerializeField] private Transform _audioSourcePrefab;

    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioClip[] sfxClips;

    private Dictionary<Music, AudioClip> musicDatabase = new Dictionary<Music, AudioClip>();
    private Dictionary<Sfx, AudioClip> sfxDatabase = new Dictionary<Sfx, AudioClip>();

    private AudioSource _musicAudioSource;
    private AudioSource _footstepAudioSource;
    private AudioSource _vacuumNoiseAudioSource;
    private AudioSource _gameOverAudioSource;

    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _instance = this;

        // Music should loop
        _musicAudioSource = GetComponent<AudioSource>();
        _musicAudioSource.loop = true;

        MapAudioClipsToEnums();

        _footstepAudioSource = _instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        _footstepAudioSource.clip = sfxDatabase[Sfx.Footsteps];

        _vacuumNoiseAudioSource = _instance.transform.GetChild(1).gameObject.GetComponent<AudioSource>();
        _vacuumNoiseAudioSource.clip = sfxDatabase[Sfx.VacuumNoise];

        _gameOverAudioSource = _instance.transform.GetChild(2).gameObject.GetComponent<AudioSource>();
        _gameOverAudioSource.clip = sfxDatabase[Sfx.GameOver];

        // This should be changed once there are multiple scenes
        //PlayMusic(Music.Menu);
    }


    private void MapAudioClipsToEnums()
    {
        musicDatabase.Add(Music.Menu, musicClips[0]);
        musicDatabase.Add(Music.Level, musicClips[1]);

        sfxDatabase.Add(Sfx.PlayerDamaged, sfxClips[0]);
        sfxDatabase.Add(Sfx.PlayerKilled, sfxClips[1]);
        sfxDatabase.Add(Sfx.PowerUp, sfxClips[2]);
        sfxDatabase.Add(Sfx.PortalSpawn, sfxClips[3]);
        sfxDatabase.Add(Sfx.PlayerShoot, sfxClips[4]);
        sfxDatabase.Add(Sfx.ItemAlert, sfxClips[5]);
        sfxDatabase.Add(Sfx.VacuumSuck, sfxClips[6]);
        sfxDatabase.Add(Sfx.VacuumNoise, sfxClips[7]);
        sfxDatabase.Add(Sfx.Footsteps, sfxClips[8]);
        sfxDatabase.Add(Sfx.GameOver, sfxClips[9]);
        sfxDatabase.Add(Sfx.DogBark, sfxClips[10]);
        sfxDatabase.Add(Sfx.EnemyDamaged, sfxClips[11]);
        sfxDatabase.Add(Sfx.Thud, sfxClips[12]);
        sfxDatabase.Add(Sfx.Dash, sfxClips[13]);
        sfxDatabase.Add(Sfx.UpgradeEaten, sfxClips[14]);
        sfxDatabase.Add(Sfx.EnemyShoot, sfxClips[15]);
        sfxDatabase.Add(Sfx.Crash, sfxClips[16]);
        sfxDatabase.Add(Sfx.OverHere, sfxClips[17]);
        sfxDatabase.Add(Sfx.HealthUp, sfxClips[18]);
    }

    public void PlayMusic(AudioManager.Music music)
    {
        _musicAudioSource.clip = musicDatabase[music];
        _musicAudioSource.Play();
    }

    public void StopMusic()
    {
        _musicAudioSource.Stop();
    }

    public void PlayFootstep()
    {
        if (!_footstepAudioSource.isPlaying)
        {
            _footstepAudioSource.Play();
        }
    }

    public void StopFootstep()
    {
        _footstepAudioSource.Stop();
    }

    public void PlayVacuum()
    {
        if (!_vacuumNoiseAudioSource.isPlaying)
        {
            _vacuumNoiseAudioSource.Play();
        }
    }

    public void StopVacuum()
    {
        _vacuumNoiseAudioSource.Stop();
    }

    public void PlayerDamaged()
    {
        PlaySfx(Sfx.PlayerDamaged);
        StartCoroutine(AudioGlitchOnPlayerDamaged());
    }

    private IEnumerator AudioGlitchOnPlayerDamaged()
    {
        _musicAudioSource.pitch = 0.8f;
        yield return new WaitForSeconds(0.25f);
        _musicAudioSource.pitch = 0.85f;
        yield return new WaitForSeconds(0.25f);
        _musicAudioSource.pitch = 0.9f;
        yield return new WaitForSeconds(0.25f);
        _musicAudioSource.pitch = 0.95f;
        yield return new WaitForSeconds(0.25f);
        _musicAudioSource.pitch = 1.0f;
    }

    public void PlayGameOverSfx()
    {
        if (!_gameOverAudioSource.isPlaying)
        {
            _gameOverAudioSource.Play();
        }
    }

    public void PlaySfx(AudioManager.Sfx sfx)
    {
        if (GameManager.instance.isPlayerAlive)
        {
            AudioSource tempAudioSource = Instantiate(_audioSourcePrefab, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<AudioSource>();
            tempAudioSource.clip = sfxDatabase[sfx];
            tempAudioSource.Play();
        }
    }

    public void PlaySfx(AudioManager.Sfx sfx, Vector3 position)
    {
        if (GameManager.instance.isPlayerAlive)
        {
            AudioSource.PlayClipAtPoint(sfxDatabase[sfx], position);
        }
    }

}