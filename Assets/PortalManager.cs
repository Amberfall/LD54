using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public static PortalManager instance;
    [SerializeField] private PortalWave[] _portalWaves;
    [SerializeField] private SpawnPoint[] _spawnPoints;
    int currentWave;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        _spawnPoints = GetComponentsInChildren<SpawnPoint>();
        currentWave = 0;
        StartCoroutine(FirstWaveTimer(5f));
    }

    public void SpawnNextWave()
    {
        if (currentWave < _portalWaves.Length)
        {
            ResetSpawnPoints();
            int portalNumber = _portalWaves[currentWave].spawnPortals.Length;
            while (portalNumber > 0)
            {
                int rnd = Random.Range(0, _spawnPoints.Length);
                if (!_spawnPoints[rnd].busy)
                {
                    Instantiate(_portalWaves[currentWave].spawnPortals[portalNumber - 1], _spawnPoints[rnd].transform.position, Quaternion.identity);
                    _spawnPoints[rnd].busy = true;
                    portalNumber--;
                }
            }
        }
        currentWave++;
    }

    private void ResetSpawnPoints()
    {
        foreach (SpawnPoint spawnPoint in _spawnPoints)
        {
            spawnPoint.busy = false;
        }
    }

    private IEnumerator FirstWaveTimer(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnNextWave();
    }

}
