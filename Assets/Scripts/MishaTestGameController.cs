using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MishaTestGameController : MonoBehaviour
{
    public GameObject prefab;
    public GameObject bag;

    private GameObject suckable1;
    // Start is called before the first frame update
    void Start()
    {

        suckable1 = Instantiate(prefab) as GameObject;
    }

    void add_suckable_1()
    {
        Debug.Log("added suckable 1");
        bag.GetComponent<BagController>().AddSuckable(suckable1.GetComponent<Suckable>());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            add_suckable_1();
        }
    }
}
