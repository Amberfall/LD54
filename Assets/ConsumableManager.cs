using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableManager : MonoBehaviour
{
    [SerializeField] private Consumable _consumable;
    [SerializeField] private Vector2 size = new Vector2(3, 3);
    [SerializeField] private float _timeToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnConsumable());
    }

    private IEnumerator SpawnConsumable()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeToSpawn);
            int n = FindObjectsOfType<Consumable>().Length;
            if (n < 4)
            {
                Instantiate(_consumable, transform.position + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), 0), Quaternion.identity);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, size);
    }
}
