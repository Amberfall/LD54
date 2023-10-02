using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FurnitureList", menuName = "ScriptableObjects/FurnitureList", order = 1)]
public class FurnitureList : ScriptableObject
{
    public float spawnRadius;
    public GameObject[] furnitures;
    public int[] weights;
}
