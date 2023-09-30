using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MishaTestGameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Wait 0.5 seconds and add a suckable to the bag
        Invoke("add_suckable", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
