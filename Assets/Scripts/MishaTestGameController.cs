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
    }

    void add_suckable_1()
    {
        GameObject succGO = Instantiate(prefab) as GameObject;
        Suckable succ = succGO.GetComponent<Suckable>();

        succ.size = Random.Range(1, 4);

        bag.GetComponent<BagController>().AddSuckable(succ);

        Debug.Log("added suckable 1");
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
