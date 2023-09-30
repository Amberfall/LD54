using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MishaTestGameController : MonoBehaviour
{
    public GameObject prefab;
    public GameObject bag;
    // Start is called before the first frame update
    void Start()
    {
    }

    void eject_suckable()
    {
        bag.GetComponent<BagController>().Eject();
    }

    void add_suckable()
    {
        GameObject succGO = Instantiate(prefab) as GameObject;
        Suckable succ = succGO.GetComponent<Suckable>();

        succ.size = Random.Range(1, 4);

        bag.GetComponent<BagController>().AddSuckable(succ);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            add_suckable();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            eject_suckable();
        }
    }
}
