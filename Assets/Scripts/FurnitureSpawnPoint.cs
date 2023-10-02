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
        int furnitureNumber = Random.Range(2, 5);
        float spawnRadius = _furnitureList.spawnRadius;
        List<GameObject> dropPositions = new List<GameObject>();
        _bubbleSpawnTransform.gameObject.SetActive(true);
        for (int i = 0; i < furnitureNumber; i++)
        {
            var go = Instantiate(_dropPositionPrefab, transform.position + new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius), 0), Quaternion.identity);
            dropPositions.Add(go);
        }
        yield return new WaitForSeconds(3f);
        _bubbleSpawnTransform.gameObject.SetActive(false);
        while (dropPositions.Count > 0)
        {
            var f = Instantiate(_furnitureList.furnitures[GetFurnitureNumber()], dropPositions[0].transform.position, Quaternion.identity);
            f.GetComponent<InanimateObject>().PlayAnimation();
            var go = dropPositions[0];
            dropPositions.RemoveAt(0);
            Destroy(go);
            yield return new WaitForSeconds(Random.Range(0, 0.5f));
        }
    }


    private int GetFurnitureNumber()
    {
        int totalWeight = GetTotalWeight();
        float rnd = Random.Range(0, 1.0f) * totalWeight;
        for (int i = 1; i < _furnitureList.weights.Length; i++)
        {
            if (rnd < _furnitureList.weights[i - 1] + _furnitureList.weights[i] && rnd > _furnitureList.weights[i - 1])
            {
                return i;
            }
        }
        return 0;
    }

    private int GetTotalWeight()
    {
        int totalWeight = 0;
        for (int i = 0; i < _furnitureList.weights.Length; i++)
        {
            totalWeight += _furnitureList.weights[i];
        }
        return totalWeight;
    }
}
