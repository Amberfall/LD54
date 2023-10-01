using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureSpawnPoint : MonoBehaviour
{
    [SerializeField] private FurnitureList _furnitureList;
    [SerializeField] private Transform _bubbleSpawnTransform;
    [SerializeField] private GameObject _dropPositionPrefab;


    public void SpawnFurnitures()
    {
        StartCoroutine(SpawnFurnituresCoroutine());
    }

    private IEnumerator SpawnFurnituresCoroutine()
    {
        int furnitureNumber = Random.Range(3, 6);
        float spawnRadius = _furnitureList.spawnRadius;
        List<GameObject> dropPositions = new List<GameObject>();
        _bubbleSpawnTransform.gameObject.SetActive(true);
        for (int i = 0; i < furnitureNumber; i++)
        {
            var go = Instantiate(_dropPositionPrefab, transform.position + new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius), 0), Quaternion.identity);
            dropPositions.Add(go);
        }
        yield return new WaitForSeconds(2.5f);
        _bubbleSpawnTransform.gameObject.SetActive(false);
        while (dropPositions.Count > 0)
        {
            Instantiate(_furnitureList.furnitures[Random.Range(0, _furnitureList.furnitures.Length)], dropPositions[0].transform.position, Quaternion.identity);
            var go = dropPositions[0];
            dropPositions.RemoveAt(0);
            Destroy(go);
            yield return new WaitForSeconds(Random.Range(0, 0.5f));
        }
    }
}
