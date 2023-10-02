using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    public string animName;
    void Start()
    {
        GetComponent<Animator>().Play(animName, 0, Random.Range(0, 1.0f));
    }
}
