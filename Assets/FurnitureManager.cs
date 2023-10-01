using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureManager : MonoBehaviour
{
    private FurnitureSpawnPoint[] _furnitureSpawnPoint;
    [SerializeField] private float _timeBetweenFurnitureSpawn;

    void Start()
    {
        _furnitureSpawnPoint = GetComponentsInChildren<FurnitureSpawnPoint>();
        StartCoroutine(SpawnFurniture());
    }

    private IEnumerator SpawnFurniture()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeBetweenFurnitureSpawn);
            _furnitureSpawnPoint[Random.Range(0, _furnitureSpawnPoint.Length)].SpawnFurnitures();
            AudioManager.Instance.PlaySfx(AudioManager.Sfx.ItemAlert);
        }
    }
}