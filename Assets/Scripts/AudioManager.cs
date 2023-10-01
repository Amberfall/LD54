using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public enum Music { Menu, Level, Death };
    public enum Sfx { PlayerDamaged, PlayerKilled, VacuumNoise, PortalSpawn, DemonDamaged, PowerUp, PlayerShoot, ItemAlert, VacuumSuck };

    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioClip[] sfxClips;

    private Dictionary<Music, AudioClip> musicDatabase = new Dictionary<Music, AudioClip>();
    private Dictionary<Sfx, AudioClip> sfxDatabase = new Dictionary<Sfx, AudioClip>();

    private AudioSource _musicAudioSource;

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

        // This should be changed once there are multiple scenes
        PlayMusic(Music.Level);
    }


    private void MapAudioClipsToEnums()
    {
        musicDatabase.Add(Music.Menu, musicClips[0]);
        musicDatabase.Add(Music.Level, musicClips[1]);
        musicDatabase.Add(Music.Death, musicClips[2]);

        sfxDatabase.Add(Sfx.PlayerDamaged, sfxClips[0]);
        sfxDatabase.Add(Sfx.PlayerKilled, sfxClips[1]);
        sfxDatabase.Add(Sfx.PowerUp, sfxClips[2]);
        sfxDatabase.Add(Sfx.PortalSpawn, sfxClips[3]);
        sfxDatabase.Add(Sfx.PlayerShoot, sfxClips[4]);
        sfxDatabase.Add(Sfx.ItemAlert, sfxClips[5]);
        sfxDatabase.Add(Sfx.VacuumSuck, sfxClips[6]);
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

    public void PlayDeathSfx()
    {
        AudioSource.PlayClipAtPoint(sfxDatabase[Sfx.PlayerKilled], new Vector3(0, 0, 0));
    }

    public void PlaySfx(AudioManager.Sfx sfx)
    {
        if (GameManager.instance.isPlayerAlive)
        {
            AudioSource.PlayClipAtPoint(sfxDatabase[sfx], new Vector3(0, 0, 0));
        }
    }

}
